import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { OwnedTicketData } from '../models/ownedticketdata.model';
import { TicketData } from '../models/ticketdata.model';

@Injectable({
  providedIn: 'root',
})
export class TicketsService {
  apiUrl = environment.apiUrl;
  httpService = inject(HttpClient);

  getAllTickets() {
    return this.httpService.get<TicketData[]>(`${this.apiUrl}/Ticket`);
  }

  getOwnedTicketsOfUser(id: number) {
    return this.httpService.get<OwnedTicketData[]>(`${this.apiUrl}/Ticket/user/${id}`);
  }

  buyTicket(id:number, ticketId: number, isPaid: boolean) {
    return this.httpService.post(`${this.apiUrl}/Ticket/${ticketId}/user/${id}/${isPaid}`, {});
  }

  payForOwnedTicket(id: number, paymentId: number) {
    return this.httpService.patch(`${this.apiUrl}/Ticket/user/${id}/pay/${paymentId}`, {});
  }
}