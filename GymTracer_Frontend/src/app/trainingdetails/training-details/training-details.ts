import { Component, inject } from '@angular/core';
import { TrainingDetailResponse } from '../models/training.detail.model';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingDetailService } from '../service/training-detail.service';
import { ThemeService } from '../../services/theme.service';
import { DatePipe, DecimalPipe, NgClass } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { TrainingApplication } from "../components/training-application/training-application";
import { UserRole } from '../../models/user.role.model';

@Component({
  selector: 'app-training-details',
  imports: [NgClass, DecimalPipe, DatePipe, TrainingApplication],
  templateUrl: './training-details.html',
  styleUrl: './training-details.css',
})
export class TrainingDetails {
  trainingId: number | null = null;
  training: TrainingDetailResponse | null = null;
  isLoading: boolean = true;
  errorMessage: string | null = null;

  isModalOpen: boolean = false;
  isCancelling = false;

  UserRole = UserRole;

  auth = inject(AuthService);
  route = inject(ActivatedRoute);
  router = inject(Router);
  trainingDetailService = inject(TrainingDetailService);
  theme = inject(ThemeService);

  ngOnInit(){
    this.route.paramMap.subscribe(params => {
      const idParam = params.get('id');
      if (idParam) {
        this.trainingId = Number.parseInt(idParam);
        this.loadTrainingDetails(this.trainingId);
      } else {
        this.router.navigate(['/trainings']);
      }
    });
  }

  endedInPast(){
    const now = new Date().getTime();
    const distance = new Date(this.training?.endTime ?? 0).getTime() - now;

    return distance <= 0;
  }

  canApply() {
    if (!this.training) return false;
    if (this.auth.actingUserRole === UserRole.trainer ||
        this.auth.actingUserRole === UserRole.staff ||
        this.auth.actingUserRole === UserRole.admin) return false;

    if (this.training.users && this.auth.actingUser) {
      return !this.training.users.some(u => u.id === this.auth.actingUser?.id);
    }

    return !this.training.isApplied;
  }

  isApplied(): boolean {
    if (!this.training) return false;
    if (this.training.users && this.auth.actingUser) {
      return this.training.users.some(u => u.id === this.auth.actingUser?.id);
    }
    return this.training.isApplied;
  }

  canCancel(): boolean {
    if (this.endedInPast()) return false;
    return this.isApplied();
  }

  canMarkPresence(): boolean {
    if (!this.training) return false;
    const role = this.auth.actingUserRole;
    return role === UserRole.trainer || role === UserRole.staff || role === UserRole.admin;
  }

  onImgError(event: Event) {
    const img = event.target as HTMLImageElement;
    img.src = 'images/training_default.jpg';
  }

  loadTrainingDetails(id: number){
    this.isLoading = true;
    this.errorMessage = null;

    this.trainingDetailService.getTrainingById(id).subscribe({
      next: (res) => {
        this.training = res;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching training details', err);
        if(err.error){
          this.errorMessage = err.error;
        }
        else{
          this.errorMessage = 'Nem sikerült betölteni az edzés adatait. Kérjük, próbálja újra később.';
        }
        this.isLoading = false;
      }
    });
  }

  goBack(){
    this.router.navigate(['/trainings']);
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal(applied = false) {
    this.isModalOpen = false;
    if (applied && this.trainingId) {
      this.loadTrainingDetails(this.trainingId);
    }
  }

  cancelApplication() {
    const userId = this.auth.actingUser?.id;
    if (!userId || !this.trainingId) return;
    this.isCancelling = true;
    this.trainingDetailService.cancelApplication(userId, this.trainingId).subscribe({
      next: () => {
        this.isCancelling = false;
        if (this.trainingId) this.loadTrainingDetails(this.trainingId);
      },
      error: (err) => {
        this.isCancelling = false;
        alert(err.error || 'Nem sikerült lemondani az edzést.');
      }
    });
  }

  onPresenceChange(userId: number, presence: boolean) {
    if (!this.trainingId) return;
    this.trainingDetailService.markPresence(this.trainingId, userId, presence).subscribe({
      error: (err) => {
        alert(err.error || 'Nem sikerült menteni a jelenlétet.');
        if (this.trainingId) this.loadTrainingDetails(this.trainingId);
      }
    });
  }

}
