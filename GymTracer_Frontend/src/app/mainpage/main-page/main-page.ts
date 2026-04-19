import { Component, inject, OnInit } from '@angular/core';
import { TicketTable } from '../ticket-table/ticket-table';
import { ThemeService } from '../../services/theme.service';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { TrainingCards } from '../training-cards/training-cards';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserModel } from '../../models/user.model';

@Component({
  selector: 'app-main-page',
  imports: [TicketTable, TrainingCards, NgClass, NgOptimizedImage, RouterLink],
  templateUrl: './main-page.html',
  styleUrl: './main-page.css',
})
export class MainPage{
  theme = inject(ThemeService)
  router = inject(Router);
  authService = inject(AuthService);

  currentUser: UserModel | null = this.authService.actingUser;
}
