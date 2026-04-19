import { Component, EventEmitter, inject, Input, Output} from '@angular/core';
import { TicketData } from '../models/ticketdata.model';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-tickets-card',
  imports: [RouterLink],
  templateUrl: './tickets-card.html',
  styleUrl: './tickets-card.css',
})
export class TicketsCard {
  router = inject(Router);

  @Input() ticket: TicketData | null = null;
  @Output() buy = new EventEmitter<TicketData>();

  buyTicket() {
    if (this.ticket) {
      this.buy.emit(this.ticket);
    }
  }
  
}
