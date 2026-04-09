import { inject, Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { UserRole } from '../models/user.role.model';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  auth = inject(AuthService);
  
  isStaffMode = false;
  isPretendMode = false;

  getColor(){
    if(this.auth.user){
      if(this.auth.user.role !== UserRole.staff && this.auth.user.role !== UserRole.admin){
        this.isStaffMode = false;
        this.isPretendMode = false;
      }
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
}
