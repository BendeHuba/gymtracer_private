import { NgClass, NgOptimizedImage } from '@angular/common';
import { Component, ElementRef, HostListener, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {MatSlideToggle, MatSlideToggleChange, MatSlideToggleModule} from '@angular/material/slide-toggle';
import { ThemeService } from '../../services/theme.service';
import { AuthService } from '../../services/auth.service';
import { UserRole } from '../../models/user.role.model';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [NgClass, RouterLink, MatSlideToggleModule],
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

  @ViewChild('burgerButton') burgerButton?: ElementRef<HTMLButtonElement>;
  @ViewChild('themeButton') themeButton?: ElementRef<HTMLButtonElement>;
  @ViewChild('staffSlide', { read: ElementRef }) staffSlide?: ElementRef;

  ngOnInit() {
    this.startCountdown();
  }

  ngOnDestroy() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }

  @HostListener('document:click',['$event'])
  onDocumentClick(event: MouseEvent){
    if(!this.burgerButton) return;

    const clickedBurger = this.burgerButton.nativeElement.contains(event.target as Node);
    if (clickedBurger) return;

    const clickedTheme = this.themeButton && this.themeButton.nativeElement.contains(event.target as Node);
    if(clickedTheme) return;

    const clickedSlide = this.staffSlide && this.staffSlide.nativeElement.contains(event.target as Node);
    if(clickedSlide) return;

    if(this.menuOpen){
      this.menuOpen = false;
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
    this.theme.setStaffMode($event.checked);
    if ($event.checked) {
      this.router.navigate(['/users']);
    } else {
      this.router.navigate(['/']);
    }
  }

  logout(){
    this.auth.HandleLogout();
    this.router.navigate(['/login']);
  }
}
