import { Component, inject, OnInit } from '@angular/core';
import { ThemeService } from '../../services/theme.service';
import { NgClass } from '@angular/common';
import { TrainingService } from '../service/training-service';
import { TrainingModel } from '../models/trainingModel';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserModel } from '../../models/user.model';

@Component({
  selector: 'app-training-cards',
  imports: [NgClass, RouterLink],
  templateUrl: './training-cards.html',
  styleUrl: './training-cards.css',
})
export class TrainingCards implements OnInit {
    
    theme = inject(ThemeService);
    trainingService = inject(TrainingService);
    authService = inject(AuthService);

    currentUser: UserModel | null = this.authService.actingUser;

    trainings: TrainingModel[] = [];

    ngOnInit(): void {
      this.getTrainings();
    }


    getTrainings() {
      let allTrainings: TrainingModel[] = [];
      let today = new Date();
      let nextWeek = new Date();
      nextWeek.setDate(today.getDate() + 7);
      console.log(today.toISOString(), nextWeek.toISOString());
      
      this.trainingService.getTrainings(today, nextWeek).subscribe({
        next: (response) => {
          allTrainings = response;
          this.trainings = allTrainings.sort(()=> 0.5 - Math.random()).slice(0, 3);
        },
        error: (error) => {
          console.log(error.url);
        }
      });
    }
    
}
