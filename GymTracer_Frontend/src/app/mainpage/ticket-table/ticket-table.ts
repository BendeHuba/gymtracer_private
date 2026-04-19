import { Component, inject, OnInit } from '@angular/core';
import { TicketsService } from '../service/ticket-service';
import { TicketModel } from '../models/ticketModel';
import { ThemeService } from '../../services/theme.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-ticket-table',
  imports: [NgClass],
  templateUrl: './ticket-table.html',
  styleUrl: './ticket-table.css',
})
export class TicketTable implements OnInit {
  theme = inject(ThemeService);
  ticketsService = inject(TicketsService);
  
  tickets: TicketModel[] = [];

  ngOnInit(): void {
    this.ticketsService.getTickets().subscribe((response) => {
      this.tickets = response.filter((ticket) => ticket.trainingId === null);
    });
  }
}
