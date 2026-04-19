import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { TicketsService } from '../services/tickets-service';
import { ThemeService } from '../../services/theme.service';
import { Router } from '@angular/router';
import { TicketData } from '../models/ticketdata.model';
import { TicketType } from '../models/tickettype.model';
import { TicketsCard } from '../tickets-card/tickets-card';
import { FormsModule } from '@angular/forms';
import { OwnedTicketCard } from '../owned-ticket-card/owned-ticket-card';
import { OwnedTicketData } from '../models/ownedticketdata.model';
import { TicketModal } from '../ticket-modal/ticket-modal';
import { PaymentModal } from '../payment-modal/payment-modal';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-tickets-page',
  imports: [NgClass, TicketsCard, FormsModule, OwnedTicketCard, TicketModal, PaymentModal],
  templateUrl: './tickets-page.html',
  styleUrl: './tickets-page.css',
})
export class TicketsPage implements OnInit {
  theme = inject(ThemeService);
  authService = inject(AuthService);
  ticketsService = inject(TicketsService);
  router = inject(Router);

  isTicketModalOpen: boolean = false;
  selectedTicketToBuy: TicketData | null = null;
  selectedOwnedTicketToPay: OwnedTicketData | null = null;

  isPaymentModalOpen: boolean = false;
  isPaidForTicket: boolean = false;

  ownedTickets: OwnedTicketData[] = [];
  standardTickets: TicketData[] = [];
  trainingTickets: TicketData[] = [];

  showStandardStudentTickets: boolean = false;
  showTrainingStudentTickets: boolean = false;

  ngOnInit(): void {
    this.getTickets();
    this.getOwnedTickets();
  }

  refreshTickets() {
    this.getTickets();
  }

  openTicketModal(ticket: TicketData) {
    this.isPaidForTicket = false;
    this.isTicketModalOpen = true;
    this.selectedOwnedTicketToPay = null;
    this.selectedTicketToBuy = ticket;
  }

  closeTicketModal() {
    this.isTicketModalOpen = false;
  }

  processPayment(isPaid: boolean) {
    this.isPaidForTicket = isPaid;
    if (!isPaid) {
      this.makeNewTicket();
    }
    else{
      this.isPaymentModalOpen = true;
    }
    this.closeTicketModal();
  }

  payOwnedTicket(ticket: OwnedTicketData){
    this.selectedTicketToBuy = null;
    this.selectedOwnedTicketToPay = ticket;
    this.isPaymentModalOpen = true;
  }

  closePaymentModal() {
    this.isPaymentModalOpen = false;
    this.selectedOwnedTicketToPay = null;
  }

  makePayment(isSuccess: boolean) {
    if (isSuccess) {
      if(this.selectedTicketToBuy){
        this.makeNewTicket();
      } else if(this.selectedOwnedTicketToPay){
        this.payForOwnedTicket();
      }
    } else {
      this.closePaymentModal();
    }
  }

  makeNewTicket(){
    
    this.ticketsService.buyTicket(this.authService.actingUser!.id, this.selectedTicketToBuy!.id, this.isPaidForTicket).subscribe({
      next: (res) => {
        this.closeTicketModal();
        this.closePaymentModal();
        this.getOwnedTickets();
      },
      error: (error) => {
        console.log(error.message);
      }
    });
  }

  payForOwnedTicket(){
    this.ticketsService.payForOwnedTicket(this.authService.actingUser!.id, this.selectedOwnedTicketToPay!.paymentId).subscribe({
      next: (res) => {
        this.closeTicketModal();
        this.closePaymentModal();
        this.getOwnedTickets();
      },
      error: (error) => {
        console.log(error.message);
      }
    });
  }

  getTickets(){
    this.ticketsService.getAllTickets().subscribe({
      next: (res) => {
        this.standardTickets = res.filter(t => t.type !== TicketType.training && t.isStudent === this.showStandardStudentTickets);
        this.trainingTickets = res.filter(t => t.type === TicketType.training && t.isStudent === this.showTrainingStudentTickets);
      },
      error: (error) => {
        console.log(error.url);
      }
    });
  }

  getOwnedTickets(){
    this.ticketsService.getOwnedTicketsOfUser(this.authService.actingUser!.id).subscribe({
      next: (res) => {
        this.ownedTickets = res;
      },
      error: (error) => {
        console.log(error.url);
      }
    });
  }
}
