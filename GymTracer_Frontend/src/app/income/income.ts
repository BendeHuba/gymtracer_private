import { Component, inject, OnInit } from '@angular/core';
import { NgClass, DecimalPipe } from '@angular/common';
import { ThemeService } from '../services/theme.service';
import { StatisticService } from '../services/statistic.service';
import { TicketStatItem } from '../models/statistic.model';

@Component({
  selector: 'app-income',
  standalone: true,
  imports: [NgClass, DecimalPipe],
  templateUrl: './income.html',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class IncomePage implements OnInit {
  theme = inject(ThemeService);
  statisticService = inject(StatisticService);

  isLoading = true;
  errorMessage: string | null = null;
  ticketStats: TicketStatItem[] = [];

  ngOnInit() {
    this.statisticService.getTicketStats().subscribe({
      next: (res) => {
        this.ticketStats = res;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.error || err.error || 'Nem sikerült betölteni a bevételi adatokat.';
        this.isLoading = false;
      }
    });
  }

  getTypeLabel(type: number): string {
    switch (type) {
      case 0: return 'Edzésjegy';
      case 1: return 'Napi';
      case 2: return 'Havi bérlet';
      case 3: return 'Alkalomjegy';
      default: return 'Ismeretlen';
    }
  }

  getTotalRevenue(): number {
    return this.ticketStats.reduce((sum, item) => sum + item.soldAmount * item.ticket.price, 0);
  }
}
