import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CardUsage } from '../models/cardusagestat.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CardusageService {
  http = inject(HttpClient);

  getCardUsage() {
    return this.http.get<CardUsage[]>(`${environment.apiUrl}/Statistic/card`);
  }
}
