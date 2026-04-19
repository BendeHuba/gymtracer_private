import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TicketData } from '../models/ticketdata.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-ticket-modal',
  imports: [FormsModule],
  templateUrl: './ticket-modal.html',
  styleUrl: './ticket-modal.css',
})
export class TicketModal {
  isPaid: boolean = false;
  
  @Input() ticket!: TicketData;

  @Output() close = new EventEmitter<void>();
  @Output() confirmPurchase = new EventEmitter< boolean >();

  closeModal() {
    this.close.emit();
  }

  buyTicket() {
    this.confirmPurchase.emit( this.isPaid );
  }
}
