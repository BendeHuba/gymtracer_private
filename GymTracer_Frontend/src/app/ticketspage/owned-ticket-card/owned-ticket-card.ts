import { Component, EventEmitter, Input, Output } from '@angular/core';
import { OwnedTicketData } from '../models/ownedticketdata.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-owned-ticket-card',
  imports: [DatePipe],
  templateUrl: './owned-ticket-card.html',
  styleUrl: './owned-ticket-card.css',
})
export class OwnedTicketCard {
  @Input() ticket: OwnedTicketData | null = null;

  @Output() payForTicket = new EventEmitter<OwnedTicketData>();

  pay() {
    if (this.ticket) {
      this.payForTicket.emit(this.ticket);
    }
  }
}
