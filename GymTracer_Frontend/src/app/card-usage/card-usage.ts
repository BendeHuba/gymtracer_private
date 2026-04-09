import { Component, inject, OnInit } from '@angular/core';
import { NgClass, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ThemeService } from '../services/theme.service';
import { StatisticService } from '../services/statistic.service';
import { CardLogItem } from '../models/statistic.model';

@Component({
  selector: 'app-card-usage',
  standalone: true,
  imports: [NgClass, DatePipe, FormsModule],
  templateUrl: './card-usage.html',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class CardUsagePage implements OnInit {
  theme = inject(ThemeService);
  statisticService = inject(StatisticService);

  isLoading = true;
  errorMessage: string | null = null;
  logs: CardLogItem[] = [];
  filtered: CardLogItem[] = [];
  filterText = '';

  ngOnInit() {
    this.statisticService.getCardLogs().subscribe({
      next: (res) => {
        this.logs = res;
        this.filtered = res;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.error || err.error || 'Nem sikerült betölteni a naplót.';
        this.isLoading = false;
      }
    });
  }

  applyFilter() {
    const term = this.filterText.toLowerCase().trim();
    if (!term) {
      this.filtered = this.logs;
      return;
    }
    this.filtered = this.logs.filter(l =>
      l.name.toLowerCase().includes(term) ||
      l.email.toLowerCase().includes(term) ||
      l.cardId.toString().includes(term) ||
      l.userId.toString().includes(term)
    );
  }
}
