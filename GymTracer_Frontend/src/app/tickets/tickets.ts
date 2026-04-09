import { Component, inject, OnInit } from '@angular/core';
import { NgClass, DatePipe, DecimalPipe } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { ThemeService } from '../services/theme.service';
import { TicketUserService } from '../services/ticket-user.service';
import { UserTicketModel, UnpaidTicketModel } from '../models/user-ticket.model';
import { TicketType } from '../trainingdetails/models/training.tickettype.model';

@Component({
  selector: 'app-tickets',
  standalone: true,
  imports: [NgClass, DatePipe, DecimalPipe],
  templateUrl: './tickets.html',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class TicketsPage implements OnInit {
  auth = inject(AuthService);
  theme = inject(ThemeService);
  ticketUserService = inject(TicketUserService);

  TicketType = TicketType;

  isLoading = true;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  tickets: UserTicketModel[] = [];
  unpaidTickets: UnpaidTicketModel[] = [];
  payingIds = new Set<number>();

  ngOnInit() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.loadAll(userId);
  }

  loadAll(id: number) {
    this.isLoading = true;
    this.errorMessage = null;
    let ticketsLoaded = false;
    let unpaidLoaded = false;
    const checkDone = () => {
      if (ticketsLoaded && unpaidLoaded) this.isLoading = false;
    };

    this.ticketUserService.getUserTickets(id).subscribe({
      next: (res) => { this.tickets = res; ticketsLoaded = true; checkDone(); },
      error: (err) => {
        if (err.status !== 404) this.errorMessage = err.error?.error || 'Nem sikerült betölteni a jegyeket.';
        ticketsLoaded = true; checkDone();
      }
    });

    this.ticketUserService.getUnpaidTickets(id).subscribe({
      next: (res) => { this.unpaidTickets = res; unpaidLoaded = true; checkDone(); },
      error: () => { unpaidLoaded = true; checkDone(); }
    });
  }

  payTicket(paymentId: number) {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.payingIds.add(paymentId);
    this.ticketUserService.payTicket(userId, paymentId).subscribe({
      next: () => {
        this.payingIds.delete(paymentId);
        this.successMessage = 'Jegy sikeresen fizetve!';
        setTimeout(() => this.successMessage = null, 3000);
        this.loadAll(userId);
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Nem sikerült fizetni.';
        this.payingIds.delete(paymentId);
      }
    });
  }

  isPaying(paymentId: number): boolean {
    return this.payingIds.has(paymentId);
  }

  getTypeLabel(type: TicketType): string {
    switch (type) {
      case TicketType.training: return 'Edzésjegy';
      case TicketType.daily: return 'Napi';
      case TicketType.monthly: return 'Havi bérlet';
      case TicketType.x_usage: return 'Alkalomjegy';
      default: return 'Ismeretlen';
    }
  }

  getTypeBadgeClass(type: TicketType): string {
    switch (type) {
      case TicketType.training: return 'bg-blue-100 text-blue-800';
      case TicketType.daily: return 'bg-green-100 text-green-800';
      case TicketType.monthly: return 'bg-purple-100 text-purple-800';
      case TicketType.x_usage: return 'bg-orange-100 text-orange-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }
}
