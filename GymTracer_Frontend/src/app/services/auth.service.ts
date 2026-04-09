import { inject, Injectable } from '@angular/core';
import { AuthUserModel } from '../models/auth.user.model';
import { UserRole } from '../models/user.role.model';
import { HttpClient } from '@angular/common/http';
import { LoginCredentials, UserLoginDto } from '../models/login.model';
import { UserModel } from '../models/user.model';
import { LogoutDto } from '../models/logout.model';
import { RegistrationCredentials, RegistrationUserDto } from '../models/registration.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  user : UserModel | null = null;
  pretendedUser : UserModel | null = null;

  token: string | null = null;
  validUntil: Date | null = null;

  http = inject(HttpClient);

  constructor() {
    const storedUser = localStorage.getItem('current_user');
    if (storedUser) {
      this.user = JSON.parse(storedUser);
    }

    const storedPretended = localStorage.getItem('pretended_user');
    if (storedPretended) {
      this.pretendedUser = JSON.parse(storedPretended);
    }

    this.token = localStorage.getItem('auth_token');
    const storedValidTo = localStorage.getItem('token_valid_to');
    if (storedValidTo) {
      this.validUntil = new Date(storedValidTo);
    }
  }

  get actingUser(){
    return this.pretendedUser ?? this.user ?? null;
  }

  get actingUserRole(){
    return this.actingUser?.role ?? UserRole.not_found;
  }

  Register(user: RegistrationCredentials){
    return this.http.post<RegistrationUserDto>(`${environment.apiUrl}/Auth/registration`, user);
  }

  Login(user: LoginCredentials){
    return this.http.post<UserLoginDto>(`${environment.apiUrl}/Auth/login`, user);
  }

  Logout(){
    return this.http.post<LogoutDto>(`${environment.apiUrl}/Auth/logout`, {});
  }

  HandleLogout(){
    this.removeSession();

    this.Logout().subscribe({
      next: () => console.log('Sikeres kijelentkezés a szerverről.'),
      error: () => console.log('Sikertelen kijelentkeztetés a szerveren, helyben sikeres'),
    });
  }

  setSession(res: UserLoginDto) {
    this.token = res.token;
    this.validUntil = new Date(res.validTo);
    this.user = res.user;
    this.pretendedUser = null;

    localStorage.setItem('auth_token', this.token);
    localStorage.setItem('token_valid_to', this.validUntil.toISOString());
    localStorage.setItem('current_user', JSON.stringify(this.user));
    localStorage.removeItem('pretended_user');
  }

  removeSession(){
    this.token = null;
    this.validUntil = null;
    this.user = null;
    this.pretendedUser = null;

    localStorage.removeItem('auth_token');
    localStorage.removeItem('token_valid_to');
    localStorage.removeItem('current_user');
    localStorage.removeItem('pretended_user');
  }

  extendSession(newValidTo: string) {
    this.validUntil = new Date(newValidTo);
    
    localStorage.setItem('token_valid_to', this.validUntil.toISOString());
  }
}
