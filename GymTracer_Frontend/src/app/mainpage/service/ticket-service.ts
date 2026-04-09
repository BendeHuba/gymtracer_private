import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TicketModel } from '../models/ticketModel';

@Injectable({
  providedIn: 'root',
})
export class TicketsService {
  apiUrl = 'http://localhost:5065/api';
  constructor(private httpService: HttpClient) {}

  getTickets() {
    return this.httpService.get<TicketModel[]>(`${this.apiUrl}/Ticket`);
  }
}
