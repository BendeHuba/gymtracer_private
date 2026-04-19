import { Component, EventEmitter, inject, Output } from '@angular/core';
import { UserdataService } from '../services/userdata-service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-user-popup',
  imports: [],
  templateUrl: './delete-user-popup.html',
  styleUrl: './delete-user-popup.css',
})
export class DeleteUserPopup {
  userdataService = inject(UserdataService);
  authService = inject(AuthService);
  router = inject(Router);

  @Output() Cancel = new EventEmitter<void>();

  CancelClick() {
    this.Cancel.emit();
  }

  //Todo: fininsh the routing
  DeleteClick() {
    this.userdataService.deleteUser(this.authService.actingUser!.id).subscribe({
      next: (res) => {
        alert("Sikeres törlés! Viszlát!");
      },
      error: (error) => {
        alert("Hiba történt a törlés során! Kérem, próbálja újra később.");
        console.log(error.message);
      }
    });
    if (this.authService.pretendedUser){
      this.authService.HandleLogout();
      this.router.navigate(['']);
    }
    this.router.navigate(['/login']);
    this.authService.HandleLogout();
  }
}
