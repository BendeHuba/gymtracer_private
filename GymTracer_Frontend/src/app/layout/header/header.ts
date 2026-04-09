import { NgClass, NgOptimizedImage } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import {MatSlideToggleChange, MatSlideToggleModule} from '@angular/material/slide-toggle';
import { ThemeService } from '../../services/theme.service';
import { AuthService } from '../../services/auth.service';
import { UserRole } from '../../models/user.role.model';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [NgClass, RouterLink, NgOptimizedImage, MatSlideToggleModule],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header implements OnInit, OnDestroy {
  theme = inject(ThemeService);
  auth = inject(AuthService);
  private router = inject(Router);

  UserRole = UserRole;

  menuOpen = false;

  remainingTime: string = '';
  private timerInterval: number | undefined;

  ngOnInit() {
    this.startCountdown();
  }

  ngOnDestroy() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }

  startCountdown() {
    const updateTimer = () => {
      if (!this.auth.validUntil) {
        this.remainingTime = '';
        return;
      }

      const now = new Date().getTime();
      const distance = this.auth.validUntil.getTime() - now;

      if (distance <= 0) {
        this.remainingTime = 'Lejárt!';

        if (this.auth.token) {
          this.auth.HandleLogout();
        }

        this.router.navigate(['/login'], { 
          state: { 
            message: 'Inaktivitás miatt automatikusan kijelentkeztettük.', 
            type: 'error'
          } 
        });
      } else {
        const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        const seconds = Math.floor((distance % (1000 * 60)) / 1000);
        
        const formattedSeconds = seconds < 10 ? '0' + seconds : seconds;
        this.remainingTime = `${minutes}:${formattedSeconds}`;
      }
    };

    updateTimer();

    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
    this.timerInterval = setInterval(updateTimer, 1000);

  }

  toggleMenu(){
    this.menuOpen = !this.menuOpen;
  }

  toggleStaff($event: MatSlideToggleChange){
    this.theme.isStaffMode = $event.checked;
  }

}
