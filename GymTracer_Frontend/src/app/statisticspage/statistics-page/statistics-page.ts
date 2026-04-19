import { Component, inject } from '@angular/core';
import { GymStatDayItem, GymStatWeekItem } from '../models/gymstatistics.model';
import { ThemeService } from '../../services/theme.service';
import { StatisticsService } from '../services/statistics-service';
import { NgClass, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-statistics-page',
  imports: [NgClass, FormsModule, DatePipe],
  templateUrl: './statistics-page.html',
  styleUrl: './statistics-page.css',
})
export class StatisticsPage {
   theme = inject(ThemeService);
  statisticService = inject(StatisticsService);

  isLoading = false;
  errorMessage: string | null = null;
  daysBack = 7;
  weeksBack = 4;

  dayStats: GymStatDayItem[] = [];
  weekStats: GymStatWeekItem[] = [];

  ngOnInit() {
    this.loadStats();
  }

  loadStats() {
    this.isLoading = true;
    this.errorMessage = null;
    this.statisticService.getGymStats(this.daysBack, this.weeksBack).subscribe({
      next: (res) => {
        this.dayStats = res.dayBackReturn;
        this.weekStats = res.weekBackReturn;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.error || err.error || 'Nem sikerült betölteni a statisztikákat.';
        this.isLoading = false;
      }
    });
  }

  getMaxDay(): number {
    return Math.max(...this.dayStats.map(d => d.guestNumber), 1);
  }

  getMaxWeek(): number {
    return Math.max(...this.weekStats.map(w => w.guestNumber), 1);
  }

  getBarHeightPixel(value: number, max: number): number {
    return Math.round(150 * (value / max));
  }
}
