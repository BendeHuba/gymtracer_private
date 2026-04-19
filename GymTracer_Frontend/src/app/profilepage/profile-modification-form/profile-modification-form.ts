import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { UserProfileModel } from '../userprofilemodels/userprofile.model';
import { ThemeService } from '../../services/theme.service';
import { DatePipe, NgClass } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { UserdataService } from '../services/userdata-service';
import { AuthService } from '../../services/auth.service';
import { UserRole } from '../../models/user.role.model';


@Component({
  selector: 'app-profile-modification-form',
  imports: [NgClass, FormsModule],
  providers: [DatePipe],
  templateUrl: './profile-modification-form.html',
  styleUrl: './profile-modification-form.css',
})
export class ProfileModificationForm implements OnInit {
  
  theme = inject(ThemeService);
  userdataService = inject(UserdataService);
  authService = inject(AuthService);
  datePipe = inject(DatePipe);

  UserRole = UserRole;

  errorMsg: string | null = null;
  isLoading = false;

  @Input() userData: UserProfileModel | null = null;
  originalUserData: UserProfileModel | null = null;
  userDataDate: string = '';
  @Output() isSuccessful = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.userDataDate = this.datePipe.transform(this.userData!.birthDate!, 'yyyy-MM-dd') || '';
    if(this.userData) this.originalUserData = {...this.userData};
  }

  cancel() {
    this.isSuccessful.emit(false);
  }

  onSubmit(userDataModifyForm: NgForm) {
    this.errorMsg = null;
    if (!userDataModifyForm.valid) {
      this.errorMsg = "Please fill out all required fields correctly.";
      return;
    }
    
    if ((this.userDataDate === '' && this.userData!.birthDate !== null)) {
      this.errorMsg = "Nem megfelelő dátumformátum. Kérem, válasszon egy érvényes dátumot.";
      return;
    }
    else if ((Number(this.datePipe.transform(this.userDataDate, 'yyyy')) < 1900) || (Number(this.datePipe.transform(this.userDataDate, 'yyyy')) > (new Date().getFullYear() - 1))){
      this.errorMsg = "A dátumnak 1900 és a tavalyi év között kell lennie.";
      return;
    }
    else if (this.userDataDate !== '' && this.userData!.birthDate === null){
      this.userData!.birthDate = new Date(this.userDataDate);
    }
    else if (this.userDataDate !== '' && this.userData!.birthDate !== null){
      this.userData!.birthDate = new Date(this.userDataDate);
    }

    const originalDateStr = this.datePipe.transform(this.originalUserData!.birthDate, 'yyyy-MM-dd') || '';
    const dateChanged = this.userDataDate !== originalDateStr;

    const dataChanged = this.userData!.name !== this.originalUserData!.name || 
                        this.userData!.email !== this.originalUserData!.email || 
                        dateChanged;
    const roleChanged = this.userData!.role !== this.originalUserData!.role;

    let changesCalled = 0;
    if (dataChanged) changesCalled++;
    if (roleChanged) changesCalled++;

    if (changesCalled === 0) {
      this.isSuccessful.emit(true);
      return; 
    }

    this.isLoading = true;
    let changesSuccessfull = 0;

    if (dataChanged) {
      this.userdataService.modifyUserData(this.authService.actingUser!.id, this.userData!).subscribe({
        next: (res) => {
          changesSuccessfull++;
          if (changesSuccessfull === changesCalled) {
            this.isLoading = false;
            this.isSuccessful.emit(true);
          }
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMsg = error.error?.error || error.error?.message || "Probléma volt a felhasználó módosítása közben, próbáld meg később!";
        }
      });
    }

    if (roleChanged) {
      this.userdataService.modifyUserRole(this.authService.actingUser!.id, this.userData!.role).subscribe({
        next: (res) => {
          changesSuccessfull++;
          if (changesSuccessfull === changesCalled) {
            this.isLoading = false;
            this.isSuccessful.emit(true);
          }
        },
        error: (error) => {
          this.isLoading = false;
          console.log(error);
          this.errorMsg = error.error?.error || error.error?.message || "Probléma volt a szerepkör módosítása közben, próbáld meg később!";
        }
      });
    }

  }
}
