import { Component, inject, OnInit } from '@angular/core';
import { NgClass, DatePipe, SlicePipe, DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { ThemeService } from '../services/theme.service';
import { TrainerService } from '../services/trainer.service';
import { TrainerTrainingModel, CreateTrainingDto, CreateTicketDto } from '../models/trainer-training.model';
import { AllTrainingResponse } from '../trainings/models/trainings.all.model';
import { Router } from '@angular/router';
import { formatErrors } from '../utils/error-helper';

@Component({
  selector: 'app-my-trainings',
  standalone: true,
  imports: [NgClass, DatePipe, FormsModule, SlicePipe, DecimalPipe],
  templateUrl: './my-trainings.html',
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
  allTrainings: AllTrainingResponse[] = [];
  deletingIds = new Set<number>();

  showModal = false;
  editingId: number | null = null;
  isSaving = false;
  modalError: string | null = null;
  fieldErrors: Record<string, string> = {};

  form: CreateTrainingDto = {
    name: '',
    description: '',
    image: '',
    startTime: '',
    endTime: '',
    maxParticipant: 10,
    tickets: []
  };

  newTicket: CreateTicketDto = {
    description: '',
    isStudent: false,
    price: 0,
    type: 0
  };

  // Timeline
  timelineTrainings: AllTrainingResponse[] = [];

  ngOnInit() {
    this.load();
  }

  load() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.isLoading = true;
    this.trainerService.getTrainerTrainings(userId).subscribe({
      next: (res) => {
        this.trainings = res;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = formatErrors(err);
        this.isLoading = false;
      }
    });
  }

  openCreate() {
    this.editingId = null;
    this.form = {
      name: '',
      description: '',
      image: '',
      startTime: '',
      endTime: '',
      maxParticipant: 10,
      tickets: []
    };
    this.newTicket = { description: '', isStudent: false, price: 0, type: 0 };
    this.modalError = null;
    this.fieldErrors = {};
    this.showModal = true;
    this.loadAllTrainings();
  }

  openEdit(t: TrainerTrainingModel) {
    this.editingId = t.id;
    this.form = {
      name: t.name,
      description: t.description,
      image: t.image,
      startTime: t.startTime.substring(0, 16),
      endTime: t.endTime.substring(0, 16),
      maxParticipant: t.maxParticipant,
      tickets: []
    };
    this.newTicket = { description: '', isStudent: false, price: 0, type: 0 };
    this.modalError = null;
    this.fieldErrors = {};
    this.showModal = true;
    this.loadAllTrainings();
  }

  loadAllTrainings() {
    this.trainerService.getAllTrainings().subscribe({
      next: (res) => {
        this.allTrainings = res;
        this.updateTimeline();
      },
      error: () => {}
    });
  }

  updateTimeline() {
    if (!this.form.startTime) {
      this.timelineTrainings = this.allTrainings;
      return;
    }
    const date = this.form.startTime.substring(0, 10);
    this.timelineTrainings = this.allTrainings.filter(t =>
      t.startTime.substring(0, 10) === date &&
      (this.editingId === null || t.id !== this.editingId)
    );
  }

  addTicket() {
    if (!this.newTicket.description || this.newTicket.price < 0) return;
    this.form.tickets.push({ ...this.newTicket });
    this.newTicket = { description: '', isStudent: false, price: 0, type: 0 };
  }

  removeTicket(index: number) {
    this.form.tickets.splice(index, 1);
  }

  closeModal() {
    this.showModal = false;
    this.modalError = null;
    this.fieldErrors = {};
  }

  save() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;

    const startDate = new Date(this.form.startTime);
    const endDate = new Date(this.form.endTime);

    if (!this.form.startTime || !this.form.endTime || isNaN(startDate.getTime()) || isNaN(endDate.getTime())) {
      this.modalError = 'Kérjük, add meg az érvényes kezdési és befejezési időpontot!';
      return;
    }

    const durationError = this.getDurationError();
    if (durationError) {
      this.modalError = durationError;
      return;
    }

    this.isSaving = true;
    this.modalError = null;
    this.fieldErrors = {};

    const dto: CreateTrainingDto = {
      ...this.form,
      startTime: startDate.toISOString(),
      endTime: endDate.toISOString(),
    };

    const obs = this.editingId
      ? this.trainerService.updateTraining(this.editingId, dto)
      : this.trainerService.createTraining(userId, dto);

    obs.subscribe({
      next: () => {
        this.isSaving = false;
        this.showModal = false;
        this.successMessage = this.editingId ? 'Edzés sikeresen módosítva!' : 'Edzés sikeresen létrehozva!';
        setTimeout(() => this.successMessage = null, 4000);
        this.load();
      },
      error: (err) => {
        this.isSaving = false;
        if (err.error?.errors && typeof err.error.errors === 'object') {
          this.fieldErrors = err.error.errors;
        } else {
          this.modalError = err.error?.error || err.error || 'Edzés mentése sikertelen.';
        }
      }
    });
  }

  deleteTraining(id: number) {
    if (!confirm('Biztosan törölni szeretnéd ezt az edzést?')) return;
    this.deletingIds.add(id);
    this.trainerService.deleteTraining(id).subscribe({
      next: () => {
        this.deletingIds.delete(id);
        this.trainings = this.trainings.filter(t => t.id !== id);
        this.successMessage = 'Edzés törölve!';
        setTimeout(() => this.successMessage = null, 3000);
      },
      error: (err) => {
        this.errorMessage = formatErrors(err);
        this.deletingIds.delete(id);
      }
    });
  }

  isDeleting(id: number): boolean {
    return this.deletingIds.has(id);
  }

  // Timeline helpers
  getTimelineStartHour(): number {
    return 6; // 6am
  }

  getTimelineEndHour(): number {
    return 22; // 10pm
  }

  getBarLeft(startTime: string): number {
    const totalMinutes = (this.getTimelineEndHour() - this.getTimelineStartHour()) * 60;
    const d = new Date(startTime);
    const minutes = (d.getHours() - this.getTimelineStartHour()) * 60 + d.getMinutes();
    return Math.max(0, Math.min(100, (minutes / totalMinutes) * 100));
  }

  getBarWidth(startTime: string, endTime: string): number {
    const totalMinutes = (this.getTimelineEndHour() - this.getTimelineStartHour()) * 60;
    const start = new Date(startTime);
    const end = new Date(endTime);
    const duration = (end.getTime() - start.getTime()) / 60000;
    return Math.max(0.5, Math.min(100, (duration / totalMinutes) * 100));
  }

  getDurationError(): string | null {
    if (!this.form.startTime || !this.form.endTime) return null;
    const start = new Date(this.form.startTime).getTime();
    const end = new Date(this.form.endTime).getTime();
    if (isNaN(start) || isNaN(end)) return null;
    const durationMs = end - start;
    if (durationMs < 5 * 60 * 1000) return 'Az edzés legalább 5 perc hosszú kell legyen!';
    if (durationMs > 5 * 60 * 60 * 1000) return 'Az edzés legfeljebb 5 óra hosszú lehet!';
    return null;
  }

  getNewBarLeft(): number {
    if (!this.form.startTime) return 0;
    return this.getBarLeft(this.form.startTime);
  }

  getNewBarWidth(): number {
    if (!this.form.startTime || !this.form.endTime) return 0;
    const start = new Date(this.form.startTime).getTime();
    const end = new Date(this.form.endTime).getTime();
    if (isNaN(start) || isNaN(end) || end <= start) return 0;
    return this.getBarWidth(this.form.startTime, this.form.endTime);
  }

  hasOverlap(): boolean {
    if (!this.form.startTime || !this.form.endTime) return false;
    const newStart = new Date(this.form.startTime).getTime();
    const newEnd = new Date(this.form.endTime).getTime();
    return this.timelineTrainings.some(t => {
      const s = new Date(t.startTime).getTime();
      const e = new Date(t.endTime).getTime();
      return newStart < e && newEnd > s;
    });
  }

  getTicketTypeLabel(type: number): string {
    switch (type) {
      case 0: return 'Edzésjegy';
      case 1: return 'Napi';
      case 2: return 'Havi';
      case 3: return 'Alkalomjegy';
      default: return '?';
    }
  }

  isUpcoming(t: TrainerTrainingModel): boolean {
    return new Date(t.endTime).getTime() > Date.now();
  }

  viewTraining(id: number) {
    this.router.navigate(['/trainings', id]);
  }

  getTicketErrors(index: number): string[] {
    const prefix = `jegy.[${index}]`;
    return Object.entries(this.fieldErrors)
      .filter(([k]) => k.startsWith(prefix))
      .map(([, v]) => v);
  }

  hasAnyTicketErrors(): boolean {
    return Object.keys(this.fieldErrors).some(k => k.startsWith('jegy.'));
  }
}
