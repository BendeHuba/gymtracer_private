import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { IncomeStatItem } from '../models/incomestat.model';

@Injectable({
  providedIn: 'root',
})
export class IncomestatService {
  http = inject(HttpClient);

  getTicketStats() {
    return this.http.get<IncomeStatItem[]>(`${environment.apiUrl}/Statistic/tickets`);
  }
}
