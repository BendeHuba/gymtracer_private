import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { GymStatResponse } from '../models/gymstatistics.model';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
    apiUrl = environment.apiUrl;
    httpService = inject(HttpClient);

    getGymStats(daysBack: number = 7, weeksBack: number = 4) {
      const params = new HttpParams()
        .set('daysBack', daysBack.toString())
        .set('weeksBack', weeksBack.toString());
      return this.httpService.get<GymStatResponse>(`${environment.apiUrl}/Statistic/gym`, { params });
    }
}
