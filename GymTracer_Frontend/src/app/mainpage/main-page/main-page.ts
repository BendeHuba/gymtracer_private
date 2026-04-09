import { Component, OnInit } from '@angular/core';
import { TicketsService } from '../service/ticket-service';
import { TicketTable } from '../ticket-table/ticket-table';

@Component({
  selector: 'app-main-page',
  imports: [TicketTable],
  templateUrl: './main-page.html',
  styleUrl: './main-page.css',
})
export class MainPage{

}
