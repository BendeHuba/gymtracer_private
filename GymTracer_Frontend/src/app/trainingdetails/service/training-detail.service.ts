import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { TrainingDetailResponse } from '../models/training.detail.model';
import { TrainingUser } from '../models/training.user.model';

@Injectable({
  providedIn: 'root',
})
export class TrainingDetailService {
  http = inject(HttpClient);

  getTrainingById(id: number){
    return this.http.get<TrainingDetailResponse>(`${environment.apiUrl}/Training/${id}`);
  }

  applyToTraining(userId: number, trainingId: number, ticketId: number){
    return this.http.post<TrainingUser>(`${environment.apiUrl}/User/${userId}/training/${trainingId}/ticket/${ticketId}`,{});
  }

  cancelApplication(userId: number, trainingId: number){
    return this.http.delete<any>(`${environment.apiUrl}/User/${userId}/training/${trainingId}`);
  }

  markPresence(trainingId: number, userId: number, presence: boolean){
    return this.http.patch<any>(`${environment.apiUrl}/Training/${trainingId}/user/${userId}/presence`, { presence });
  }
}
