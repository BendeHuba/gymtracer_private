import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TicketModel } from '../models/ticketModel';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TicketsService {
  apiUrl = environment.apiUrl;
  httpService = inject(HttpClient);

  getTickets() {
    return this.httpService.get<TicketModel[]>(`${this.apiUrl}/Ticket`);
  }
}
