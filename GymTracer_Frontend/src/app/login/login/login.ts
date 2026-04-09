import { NgClass } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ThemeService } from '../../services/theme.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink, NgClass],
  templateUrl: './login.html',
  styleUrl: './login.css',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class Login implements OnInit {
  private router = inject(Router);
  theme = inject(ThemeService);
  auth = inject(AuthService)

  credentials = {
    email: '',
    password: ''
  };

  alertMessage: string | null = null;
  alertType: 'error' | 'success' = 'error';
  isLoading: boolean = false;

  ngOnInit() {
    const state = history.state;
    if (state && state.message) {
      this.alertMessage = state.message;
      this.alertType = state.type || 'success';
    }
  }

  onSubmit(loginForm: NgForm) {
    this.alertMessage = null;

    if (loginForm.valid) {
      this.isLoading = true;

      this.auth.Login(this.credentials).subscribe({
        next: (res) => {
          this.isLoading = false;
          
          this.auth.setSession(res);

          this.router.navigate(['/']);
        },
        error: (err) => {
          this.isLoading = false;
          this.alertType = 'error';

          if (err.error && err.error.error) {
            this.alertMessage = err.error.error;
          } else {
            this.alertMessage = 'Szerverhiba történt. Kérjük, próbálja újra később!';
          }
        }
      });
    } else {
      Object.values(loginForm.controls).forEach(control => {
        control.markAsTouched();
      });
    }
  }
}