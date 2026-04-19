import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { TrainerTrainingModel, CreateTrainingDto, UpdateTrainingDto } from '../models/trainer-training.model';
import { AllTrainingResponse } from '../trainings/models/trainings.all.model';

@Injectable({ providedIn: 'root' })
export class TrainerService {
  http = inject(HttpClient);

  getTrainerTrainings(trainerId: number) {
    return this.http.get<TrainerTrainingModel[]>(`${environment.apiUrl}/Training/user/${trainerId}`);
  }

  getAllTrainings(start?: Date, end?: Date) {
    let params = new HttpParams();
    
    if (start) params = params.set('start', start.toISOString());
    if (end) params = params.set('end', end.toISOString());

    return this.http.get<AllTrainingResponse[]>(`${environment.apiUrl}/Training`, { params });
  }

  createTraining(trainerId: number, data: CreateTrainingDto) {
    return this.http.post(`${environment.apiUrl}/Training/user/${trainerId}`, data);
  }

  updateTraining(trainingId: number, data: UpdateTrainingDto) {
    return this.http.put(`${environment.apiUrl}/Training/${trainingId}`, data);
  }

  deleteTraining(trainingId: number) {
    return this.http.delete(`${environment.apiUrl}/Training/${trainingId}`);
  }
}
