import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TicketData } from '../models/ticketdata.model';
import { FormsModule } from '@angular/forms';
import { OwnedTicketData } from '../models/ownedticketdata.model';

@Component({
  selector: 'app-payment-modal',
  imports: [FormsModule],
  templateUrl: './payment-modal.html',
  styleUrl: './payment-modal.css',
})
export class PaymentModal {
  @Input() newTicket: TicketData | null = null;
  @Input() ownedTicket: OwnedTicketData | null = null;

  @Output() paymentSuccess = new EventEmitter<boolean>();

  nameOnCard: string = '';
  cardNumber: string = '';
  expirationDate: string = '';
  cvc: string = '';

  errorMsg:string | null = null;

  close(){
    this.paymentSuccess.emit(false);
  }

  onPaymentSubmit(userPaymentForm: any) {
    if (!userPaymentForm.valid) {
      this.errorMsg = "Töltsön ki minden mezőt a fizetéshez!";
      return;
    }
    this.paymentSuccess.emit(true);
  }
}
