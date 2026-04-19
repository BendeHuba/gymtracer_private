import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { OwnedTicketData } from '../userprofilemodels/userticket.model';

@Component({
  selector: 'app-tickets-card',
  imports: [DatePipe],
  templateUrl: './tickets-card.html',
  styleUrl: './tickets-card.css',
})
export class TicketsCard {
  @Input({ required: true }) ticket!: OwnedTicketData;
  @Input() isPaying = false;
  
  @Output() localPay = new EventEmitter<OwnedTicketData>();
}
