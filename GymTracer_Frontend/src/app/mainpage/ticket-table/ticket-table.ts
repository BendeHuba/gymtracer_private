import { Component, OnInit } from '@angular/core';
import { TicketsService } from '../service/ticket-service';
import { TicketModel } from '../models/ticketModel';

@Component({
  selector: 'app-ticket-table',
  imports: [],
  templateUrl: './ticket-table.html',
  styleUrl: './ticket-table.css',
})
export class TicketTable implements OnInit {
  tickets: TicketModel[] = [];

  constructor(private ticketsService: TicketsService) {}

  ngOnInit(): void {
    this.ticketsService.getTickets().subscribe((response) => {
      console.log(response);
      
      this.tickets = response.filter((ticket) => ticket.trainingId === null);
    });
  }
}
