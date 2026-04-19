import { Component, inject, OnInit } from '@angular/core';
import { UserdataService } from '../services/userdata-service';
import { ThemeService } from '../../services/theme.service';
import { DatePipe, NgClass } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { UserProfileModel } from '../userprofilemodels/userprofile.model';
import { CardsList } from '../cards-list/cards-list';
import { ProfileModificationForm } from '../profile-modification-form/profile-modification-form';
import { Router } from '@angular/router';
import { DeleteUserPopup } from '../delete-user-popup/delete-user-popup';
import { UserRole } from '../../models/user.role.model';
import { TicketsCard } from "../tickets-card/tickets-card";
import { OwnedTicketData } from '../userprofilemodels/userticket.model';
import { Usertickets } from '../services/usertickets';
import { FormsModule } from '@angular/forms';
import { CardDataModel } from '../userprofilemodels/carddata.model';
import { GateService } from '../services/gate-service';
import { Usage_Gate } from '../userprofilemodels/gate.enum.model';

@Component({
  selector: 'app-profile-page',
  imports: [NgClass, FormsModule, DatePipe, CardsList, ProfileModificationForm, DeleteUserPopup, TicketsCard],
  templateUrl: './profile-page.html',
  styleUrl: './profile-page.css',
})
export class ProfilePage implements OnInit {
  theme = inject(ThemeService);
  authService = inject(AuthService);
  userdataService = inject(UserdataService);
  router = inject(Router);
  userticketService = inject(Usertickets);
  gateService = inject(GateService);

  userData: UserProfileModel | null = null;
  copyOfUserData: UserProfileModel | null = null;
  isModifing = false;
  isDeleteing = false;
  isLoading = false;

  tickets: OwnedTicketData[] = [];
  get entryTickets(){
    return this.tickets.filter(t => t.trainingId == null);
  }
  entryWithoutTicket = false;

  paymentId: number | null = null;

  isGateLoading = false;
  gateMessage: { text: string; isError: boolean } | null = null;
  
  cards: CardDataModel[] = [];
  mainGateId: string | number = 0;

  ngOnInit(): void {
    this.isLoading = true;
    this.userdataService.getUserData(this.authService.actingUser!.id).subscribe({
      next: (res) => {
        this.userData = res;
        if(this.authService.pretendedUser) this.authService.setPretendedUser(res);
        else this.authService.setUser(res);

        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
      }
    });

    if(this.authService.pretendedUser){
      this.userticketService.getOwnedTicketsOfUser(this.authService.actingUser!.id).subscribe({
        next: (res) => {
          this.tickets = res;
        },
        error: (error) => {
          console.log(error);
        }
      })
    }
  }

  toggleModification() {
    this.isModifing = true;
    this.copyOfUserData = JSON.parse(JSON.stringify(this.userData));
  }

  isSuccessful($event: boolean) {
    this.isLoading = true;
    this.isModifing = false;
    this.userdataService.getUserData(this.authService.actingUser!.id).subscribe({
      next: (res) => {
        this.userData = res;
        if(this.authService.pretendedUser) this.authService.setPretendedUser(res);
        else this.authService.setUser(res);

        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
      }
    });
  }

  deleteProfile() {
    this.isDeleteing = true;
  }

  deleteCanceled(){
    this.isDeleteing = false;
  }

  getRoleLabel(role: UserRole) {
    switch(role) {
      case UserRole.admin: return 'Admin';
      case UserRole.staff: return 'Személyzet';
      case UserRole.trainer: return 'Edző';
      case UserRole.customer: return 'Vendég';
      default: return '?';
    }
  }

  getRoleBadgeClass(role: UserRole) {
    switch(role) {
      case UserRole.admin: return 'bg-purple-100 text-purple-800';
      case UserRole.staff: return 'bg-yellow-100 text-yellow-800';
      case UserRole.trainer: return 'bg-blue-100 text-blue-800';
      case UserRole.customer: return 'bg-green-100 text-green-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }

  onLocalPay(ticket: OwnedTicketData) {
    if (this.paymentId !== null) return;

    const userId = this.authService.actingUser!.id;
    this.paymentId = ticket.paymentId;

    this.userticketService.payForOwnedTicket(userId, this.paymentId).subscribe({
      next: () => {
        const index = this.tickets.findIndex(t => t.paymentId === ticket.paymentId);
        if (index !== -1) {
          this.tickets[index].isPayed = true;
        }
        this.paymentId = null;
      },
      error: (err) => {
        this.paymentId = null;
      }
    });
  }

  openGate() {
    console.log('asd')
    if (!this.cards || this.cards.length === 0) {
      this.gateMessage = { 
        text: 'Nincs kártya társítva ehhez a felhasználóhoz! Kérjük, előbb rendeljen hozzá egyet.', 
        isError: true 
      };
      return;
    }

    const cardCode = this.cards[0].code;

    this.isGateLoading = true;
    this.gateMessage = null;

    this.gateService.enterGate(Usage_Gate.main_entrance, cardCode, this.entryWithoutTicket, true).subscribe({
        next: (response) => {
          this.gateMessage = { 
            text: response.message || 'Sikeres beléptetés!', 
            isError: false 
          };
          setTimeout(() => this.gateMessage = null, 3000);

          this.isGateLoading = false;
          if(this.userData) this.userData.wentInToday = true;
        },
        error: (err) => {
          let errorMsg = 'Ismeretlen hiba történt.';
          
          if (typeof err.error === 'string') {
            errorMsg = err.error;
          } else if (err.error?.message) {
            errorMsg = err.error.message;
          } else if (err.error?.errors) {
            errorMsg = Object.values(err.error.errors).flat().join(', ');
          }

          this.gateMessage = { 
            text: errorMsg, 
            isError: true 
          };

          this.isGateLoading = false;
        }
      });
  }
}
