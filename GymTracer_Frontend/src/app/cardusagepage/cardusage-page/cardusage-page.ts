import { Component, inject } from '@angular/core';
import { ThemeService } from '../../services/theme.service';
import { CardusageService } from '../services/cardusage-service';
import { CardUsage } from '../models/cardusagestat.model';
import { FormsModule } from '@angular/forms';
import { DatePipe, NgClass } from '@angular/common';

@Component({
  selector: 'app-cardusage-page',
  imports: [NgClass, DatePipe, FormsModule],
  templateUrl: './cardusage-page.html',
  styleUrl: './cardusage-page.css',
})
export class CardusagePage {
  theme = inject(ThemeService);
  statisticService = inject(CardusageService);

  isLoading = true;
  errorMessage: string | null = null;
  logs: CardUsage[] = [];
  filtered: CardUsage[] = [];
  filterText = '';

  ngOnInit() {
    this.statisticService.getCardUsage().subscribe({
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
