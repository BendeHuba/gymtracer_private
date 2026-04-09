import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AllTrainingResponse, AllTrainingFilter } from '../models/trainings.all.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TrainingService {
  http = inject(HttpClient);

  public lastFilters: AllTrainingFilter | null = null;
  public lastResults: AllTrainingResponse[] = [];
  public hasSearched = false;
  
  public preserveState = false;

  getAllTrainings(filter: AllTrainingFilter = {}){
    let params = new HttpParams();

    if (filter.start) {
      const startStr = filter.start instanceof Date ? filter.start.toISOString() : filter.start;
      params = params.set('start', startStr);
    }

    if (filter.end) {
      const endStr = filter.end instanceof Date ? filter.end.toISOString() : filter.end;
      params = params.set('end', endStr);
    }

    if (filter.trainerName) {
      params = params.set('trainerName', filter.trainerName);
    }

    if (filter.trainerId !== null && filter.trainerId !== undefined) {
      params = params.set('trainerId', filter.trainerId.toString());
    }

    if (filter.keyword) {
      params = params.set('keyword', filter.keyword);
    }

    return this.http.get<AllTrainingResponse[]>(`${environment.apiUrl}/Training`, { params });
  }

  clearState(){
    this.lastFilters = null;
    this.lastResults = [];
    this.hasSearched = false;
  }
}
