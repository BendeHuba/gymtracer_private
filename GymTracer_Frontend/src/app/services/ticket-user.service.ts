import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserTicketModel, UnpaidTicketModel } from '../models/user-ticket.model';

@Injectable({ providedIn: 'root' })
export class TicketUserService {
  http = inject(HttpClient);

  getUserTickets(userId: number) {
    return this.http.get<UserTicketModel[]>(`${environment.apiUrl}/Ticket/user/${userId}`);
  }

  getUnpaidTickets(userId: number) {
    return this.http.get<UnpaidTicketModel[]>(`${environment.apiUrl}/Ticket/user/${userId}/unpaid`);
  }

  payTicket(userId: number, paymentId: number) {
    return this.http.patch<any>(`${environment.apiUrl}/Ticket/user/${userId}/pay/${paymentId}`, {});
  }

  purchaseTicket(userId: number, ticketId: number, isPaid: boolean) {
    return this.http.post<any>(`${environment.apiUrl}/Ticket/${ticketId}/user/${userId}/${isPaid}`, {});
  }
}
