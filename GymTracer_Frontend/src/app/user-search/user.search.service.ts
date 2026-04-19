import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserSearchResult } from './models/user-search.model';

@Injectable({ providedIn: 'root' })
export class UserSearchService {
  http = inject(HttpClient);

  searchUsers(name?: string, email?: string, guid?: string) {
    let params = new HttpParams();
    if (name) params = params.set('name', name);
    if (email) params = params.set('email', email);
    if (guid) params = params.set('guid', guid);
    return this.http.get<UserSearchResult[]>(`${environment.apiUrl}/User`, { params });
  }
}
