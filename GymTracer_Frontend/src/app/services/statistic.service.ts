import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { GymStatResponse, TicketStatItem, CardLogItem } from '../models/statistic.model';

@Injectable({ providedIn: 'root' })
export class StatisticService {
  http = inject(HttpClient);

  getGymStats(daysBack: number, weeksBack: number) {
    const params = new HttpParams()
      .set('daysBack', daysBack.toString())
      .set('weeksBack', weeksBack.toString());
    return this.http.get<GymStatResponse>(`${environment.apiUrl}/Statistic/gym`, { params });
  }

  getTicketStats() {
    return this.http.get<TicketStatItem[]>(`${environment.apiUrl}/Statistic/tickets`);
  }

  getCardLogs() {
    return this.http.get<CardLogItem[]>(`${environment.apiUrl}/Statistic/card`);
  }
}
