import { NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ThemeService } from '../../services/theme.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-registration',
  imports: [FormsModule, RouterLink, NgClass],
  templateUrl: './registration.html',
  styleUrl: './registration.css',
  host: { class: 'flex-1 flex flex-col w-full' } 
})
export class Registration {
  private router = inject(Router);
  theme = inject(ThemeService);
  auth = inject(AuthService);

  credentials = {
    name: '',
    email: '',
    password: ''
  };

  errorMessage: string | null = null;
  isLoading: boolean = false;

  onSubmit(registerForm: NgForm) {
    this.errorMessage = null;

    if (registerForm.valid) {
      this.isLoading = true;

      this.auth.Register(this.credentials).subscribe({
        next: (res) => {
          this.isLoading = false;
          
          this.router.navigate(['/login'], { 
            state: { 
              message: 'Sikeres regisztráció! Kérjük, jelentkezzen be.', 
              type: 'success' 
            } 
          });
        },
        error: (err) => {
          this.isLoading = false;

          if (err.error && err.error.error) {
            this.errorMessage = err.error.error;
          } else {
            this.errorMessage = 'Szerverhiba történt. Kérjük, próbálja újra később!';
          }
        }
      });
    } else {
      Object.values(registerForm.controls).forEach(control => {
        control.markAsTouched();
      });
    }
  }
}