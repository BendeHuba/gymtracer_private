import { Component, inject, OnInit } from '@angular/core';
import { NgClass, SlicePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ThemeService } from '../services/theme.service';
import { StatisticService } from '../services/statistic.service';
import { GymStatDayItem, GymStatWeekItem } from '../models/statistic.model';

@Component({
  selector: 'app-statistics',
  standalone: true,
  imports: [NgClass, FormsModule, SlicePipe],
  templateUrl: './statistics.html',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class StatisticsPage implements OnInit {
  theme = inject(ThemeService);
  statisticService = inject(StatisticService);

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

  getBarHeightPercent(value: number, max: number): number {
    return Math.round((value / max) * 100);
  }
}
