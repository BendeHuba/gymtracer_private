import { inject, Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { UserRole } from '../models/user.role.model';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  auth = inject(AuthService);
  
  isDarkMode = localStorage.getItem('dark_mode') === 'true';
  isStaffMode = localStorage.getItem('staff_mode') === 'true';

  constructor() {
    if (this.isDarkMode) {
      document.body.classList.add('dark');
    } else {
      document.body.classList.remove('dark');
    }
  }
  
  get isPretendMode(){
    return this.auth.pretendedUser !== null;
  }

  setStaffMode(value: boolean) {
    this.isStaffMode = value;
    localStorage.setItem('staff_mode', String(value));
  }

  getColor(){
    if(this.auth.user){
      if(this.auth.user.role !== UserRole.staff && this.auth.user.role !== UserRole.admin){
        this.setStaffMode(false);
        return "";
      }
    }
    else{
      this.setStaffMode(false);
      return "";
    }

    if (this.isStaffMode){
      if(this.isPretendMode){
        return "theme-orange"
      }
      else{
        return "theme-yellow"
      }
    }
    return "";
  }

  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;
    localStorage.setItem('dark_mode', String(this.isDarkMode));
    
    if (this.isDarkMode) {
      document.body.classList.add('dark');
    } else {
      document.body.classList.remove('dark');
    }
  }
}
