import { Component, inject } from '@angular/core';
import { TrainingDetailResponse } from '../models/training.detail.model';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingDetailService } from '../service/training-detail.service';
import { ThemeService } from '../../services/theme.service';
import { DatePipe, DecimalPipe, NgClass } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { TrainingApplication } from "../components/training-application/training-application";
import { UserRole } from '../../models/user.role.model';
import { AddEditTraining } from "../../global-components/add-edit-training/add-edit-training";
import { TrainerService } from '../../services/trainer.service';
import { formatErrors } from '../../utils/error-helper';
import { TrainerTrainingModel } from '../../models/trainer-training.model';
import { environment } from '../../../environments/environment';
import { AllTrainingResponse } from '../../trainings/models/trainings.all.model';

@Component({
  selector: 'app-training-details',
  imports: [NgClass, DecimalPipe, DatePipe, TrainingApplication, AddEditTraining],
  templateUrl: './training-details.html',
  styleUrl: './training-details.css',
})
export class TrainingDetails {
  trainingId: number | null = null;
  training: TrainingDetailResponse | null = null;
  isLoading = true;
  errorMessage: string | null = null;

  isModalOpen = false;
  isCancelling = false;

  allTrainings: AllTrainingResponse[] = []

  showEditModal = false;
  editSuccessMessage: string | null = null; 
  editingTraining: TrainerTrainingModel | null = null;
  isDeleting = false;

  UserRole = UserRole;

  auth = inject(AuthService);
  route = inject(ActivatedRoute);
  router = inject(Router);
  trainingDetailService = inject(TrainingDetailService);
  theme = inject(ThemeService);
  trainerService = inject(TrainerService);

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
    if (this.auth.actingUser?.id == this.training.trainer.id) return false;

    if (this.training.users && this.auth.actingUser) {
      return !this.training.users.some(u => u.id === this.auth.actingUser?.id);
    }

    return !this.training.isApplied;
  }

  canEditTraining() {
    if (!this.training || !this.auth?.actingUser) return false;
  
    const user = this.auth.actingUser;
    return user.id === this.training.trainer.id || 
        user.role === UserRole.admin || 
        user.role === UserRole.staff;
  }

  openEdit() {
    this.editingTraining = this.training;
    this.showEditModal = true;
    this.loadAllTrainings();
  }

  loadAllTrainings() {
    const startDate = new Date();
    startDate.setDate(startDate.getDate() - environment.pastTrainingDays);    
    const endDate = new Date();
    endDate.setDate(endDate.getDate() + environment.futureTrainingDays);

    this.trainerService.getAllTrainings(startDate, endDate).subscribe({
      next: (res) => {
        this.allTrainings = res.map(t => ({
          ...t,
          startTime: this.ensureUtc(t.startTime),
          endTime: this.ensureUtc(t.endTime)
        }));
      },
      error: () => {}
    });
  }

  onModalClosed() {
    this.showEditModal = false;
  }

  onSaveSuccess(message: string) {
    this.showEditModal = false;
    this.editSuccessMessage = message;
    setTimeout(() => this.editSuccessMessage = null, 4000);
    if (this.trainingId) {
      this.loadTrainingDetails(this.trainingId);
    }
  }

  inProgress() {
    if (!this.training?.startTime || !this.training?.endTime) return false;
    const now = new Date().getTime();
    const start = new Date(this.training?.startTime).getTime();
    const end = new Date(this.training?.endTime).getTime();
    return start <= now && now < end;
  }

  deleteTraining() {
    if(!this.training) return;
    if (!confirm('Biztosan törölni szeretnéd ezt az edzést?')) return;
    this.isDeleting = true;
    this.trainerService.deleteTraining(this.training.id).subscribe({
      next: () => {
        this.isDeleting = false;
        this.goBack();
      },
      error: (err) => {
        this.isDeleting = false;
        this.errorMessage = formatErrors(err);
      }
    });
  }

  isApplied(){
    if (!this.training) return false;
    if (this.training.users && this.auth.actingUser) {
      return this.training.users.some(u => u.id === this.auth.actingUser?.id);
    }
    return this.training.isApplied;
  }

  canCancel(){
    if (this.endedInPast()) return false;
    return this.isApplied();
  }

  canMarkPresence(){
    if (!this.training) return false;
    const role = this.auth.actingUserRole;
    return role === UserRole.trainer || role === UserRole.staff || role === UserRole.admin;
  }

  isPresenceCheckable(){
    if (!this.training) return false;
    const now = Date.now();
    const endTime = new Date(this.training.endTime).getTime();
    const twoWeeksMs = 14 * 24 * 60 * 60 * 1000;
    return now <= endTime + twoWeeksMs;
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
        this.training = {
          ...res,
          startTime: this.ensureUtc(res.startTime),
          endTime: this.ensureUtc(res.endTime)
        };
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

  openModal(){
    this.isModalOpen = true;
  }

  closeModal(applied = false){
    this.isModalOpen = false;
    if (applied && this.trainingId) {
      this.loadTrainingDetails(this.trainingId);
    }
  }

  cancelApplication(){
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

  onPresenceChange(userId: number, presence: boolean){
    if (!this.trainingId) return;
    this.trainingDetailService.markPresence(this.trainingId, userId, presence).subscribe({
      error: (err) => {
        alert(err.error || 'Nem sikerült menteni a jelenlétet.');
        if (this.trainingId) this.loadTrainingDetails(this.trainingId);
      }
    });
  }

  private ensureUtc(dateString: string){
    if (!dateString) return dateString;

    const isoString = dateString.replace(' ', 'T');
    return isoString.endsWith('Z') ? isoString : isoString + 'Z';
  }
}
