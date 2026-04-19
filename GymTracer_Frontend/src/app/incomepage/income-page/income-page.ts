import { DecimalPipe, NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ThemeService } from '../../services/theme.service';
import { IncomestatService } from '../Services/incomestat-service';
import { IncomeStatItem } from '../models/incomestat.model';

@Component({
  selector: 'app-income-page',
  imports: [NgClass, DecimalPipe],
  templateUrl: './income-page.html',
  styleUrl: './income-page.css',
})
export class IncomePage {
  theme = inject(ThemeService);
  statisticService = inject(IncomestatService);

  isLoading = true;
  errorMessage: string | null = null;
  ticketStats: IncomeStatItem[] = [];

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
