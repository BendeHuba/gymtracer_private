import { Component, inject, OnInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { ThemeService } from '../../services/theme.service';
import { TrainerService } from '../../services/trainer.service';
import { TrainerTrainingModel } from '../../models/trainer-training.model';
import { AllTrainingResponse } from '../../trainings/models/trainings.all.model';
import { Router } from '@angular/router';
import { formatErrors } from '../../utils/error-helper';
import { MyTrainingList } from '../components/my-training-list/my-training-list';
import { AddEditTraining } from '../../global-components/add-edit-training/add-edit-training';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-my-trainings',
  imports: [NgClass, MyTrainingList, AddEditTraining],
  templateUrl: './my-trainings.html',
  styleUrl: './my-trainings.css',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class MyTrainingsPage implements OnInit {
  auth = inject(AuthService);
  theme = inject(ThemeService);
  trainerService = inject(TrainerService);
  router = inject(Router);

  isLoading = true;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  trainings: TrainerTrainingModel[] = [];
  deletingIds = new Set<number>();

  showModal = false;
  editingTraining: TrainerTrainingModel | null = null;
  allTrainings: AllTrainingResponse[] = [];

  ngOnInit() {
    this.load();
  }

  load() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.isLoading = true;
    this.trainerService.getTrainerTrainings(userId).subscribe({
      next: (res) => {
        this.trainings = res.map(t => ({
          ...t,
          startTime: this.ensureUtc(t.startTime),
          endTime: this.ensureUtc(t.endTime)
        }));
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = formatErrors(err);
        this.isLoading = false;
      }
    });
  }

  openCreate() {
    this.editingTraining = null;
    this.showModal = true;
    this.loadAllTrainings();
  }

  openEdit(t: TrainerTrainingModel) {
    this.editingTraining = t;
    this.showModal = true;
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
    this.showModal = false;
  }

  onSaveSuccess(message: string) {
    this.showModal = false;
    this.successMessage = message;
    setTimeout(() => this.successMessage = null, 4000);
    this.load();
  }

  deleteTraining(id: number) {
    if (!confirm('Biztosan törölni szeretnéd ezt az edzést?')) return;
    this.deletingIds = new Set([...this.deletingIds, id]);
    this.trainerService.deleteTraining(id).subscribe({
      next: () => {
        this.deletingIds = new Set([...this.deletingIds].filter(x => x !== id));
        this.trainings = this.trainings.filter(t => t.id !== id);
        this.successMessage = 'Edzés törölve!';
        setTimeout(() => this.successMessage = null, 3000);
      },
      error: (err) => {
        this.errorMessage = formatErrors(err);
        this.deletingIds = new Set([...this.deletingIds].filter(x => x !== id));
      }
    });
  }

  viewTraining(id: number) {
    this.router.navigate(['/trainings', id]);
  }

  private ensureUtc(dateString: string){
    if (!dateString) return dateString;
    const isoString = dateString.replace(' ', 'T');
    return isoString.endsWith('Z') ? isoString : isoString + 'Z';
  }
}
