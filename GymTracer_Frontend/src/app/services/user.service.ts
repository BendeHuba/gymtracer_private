import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserProfileModel } from '../models/user-profile.model';
import { CardModel } from '../models/card.model';
import { UserSearchResult } from '../models/user-search.model';
import { UserModel } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class UserService {
  http = inject(HttpClient);

  getProfile(id: number) {
    return this.http.get<UserProfileModel>(`${environment.apiUrl}/User/${id}/profile`);
  }

  updateProfile(id: number, data: { name: string; email: string; birthDate?: string | null }) {
    return this.http.put<UserProfileModel>(`${environment.apiUrl}/User/${id}/profile`, data);
  }

  getCards(id: number) {
    return this.http.get<CardModel[]>(`${environment.apiUrl}/User/${id}/card`);
  }

  createCard(id: number) {
    return this.http.post<CardModel[]>(`${environment.apiUrl}/User/${id}/card`, {});
  }

  deleteCard(id: number, cardId: number) {
    return this.http.delete(`${environment.apiUrl}/User/${id}/card/${cardId}`);
  }

  getUserTrainings(id: number, arePreviousNeeded: boolean) {
    return this.http.get<any[]>(`${environment.apiUrl}/User/${id}/training`, {
      params: new HttpParams().set('arePreviousNeeded', arePreviousNeeded.toString())
    });
  }

  searchUsers(name?: string, email?: string) {
    let params = new HttpParams();
    if (name) params = params.set('name', name);
    if (email) params = params.set('email', email);
    return this.http.get<UserSearchResult[]>(`${environment.apiUrl}/User`, { params });
  }
}
