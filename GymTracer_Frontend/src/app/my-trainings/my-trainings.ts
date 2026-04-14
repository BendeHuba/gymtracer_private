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
  imports: [NgClass, DatePipe, FormsModule, DecimalPipe, SlicePipe],
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

  timelineTrainings: AllTrainingResponse[] = [];

  editingTicketIndex: number | null = null;
  ticketError: string | null = null;

  isBackdropMousedown = false;

  onBackdropMouseDown(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.isBackdropMousedown = true;
    }
  }

  onBackdropClick(event: MouseEvent) {
    if (event.target === event.currentTarget && this.isBackdropMousedown) {
      this.closeModal();
    }
    this.isBackdropMousedown = false;
  }

  editTicket(index: number) {
    if (this.editingTicketIndex !== null && this.editingTicketIndex !== index) {
      const currentTicket = this.form.tickets[this.editingTicketIndex];
      
      if (!currentTicket.description || currentTicket.description.trim() === '' || currentTicket.price === null || currentTicket.price === undefined || currentTicket.price < 0) {
        this.ticketError = 'Kérjük, előbb töltsd ki helyesen a jelenleg szerkesztett jegyet!';
        return;
      }
    }

    this.editingTicketIndex = index;
    this.ticketError = null;
  }

  saveTicketEdit() {
    this.editingTicketIndex = null;
    this.ticketError = null;
  }

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
    this.editingId = null;
    this.editingTicketIndex = null;

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
    this.editingTicketIndex = null;
    
    this.form = {
      name: t.name,
      description: t.description,
      image: t.image,
      startTime: this.toLocalDatetimeString(t.startTime),
      endTime: this.toLocalDatetimeString(t.endTime),
      maxParticipant: t.maxParticipant,
      tickets: t.tickets && t.tickets.length > 0
        ? t.tickets.map(ticket => ({ ...ticket }))
        : []
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
        this.allTrainings = res.map(t => ({
          ...t,
          startTime: this.ensureUtc(t.startTime),
          endTime: this.ensureUtc(t.endTime)
        }));
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
    this.form.tickets.push({ ...this.newTicket, price : this.newTicket.price || 0 });
    this.newTicket = { description: '', isStudent: false, price: 0, type: 0 };
  }

  removeTicket(index: number) {
    if (!confirm('Biztosan törölni szeretnéd ezt a jegyet?')) {
      return;
    }
    
    this.form.tickets.splice(index, 1);
  }

  closeModal() {
    this.showModal = false;
    this.modalError = null;
    this.fieldErrors = {};

    this.editingTicketIndex = null;
  }

  save() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;

    this.fieldErrors = {};
    this.modalError = null;
    this.ticketError = null;
    let isValid = true;

    if (!this.form.name || this.form.name.trim() === '') {
      this.fieldErrors['Name'] = 'Az edzés nevének megadása kötelező!';
      isValid = false;
    }
    if (!this.form.description || this.form.description.trim() === '') {
      this.fieldErrors['Description'] = 'Az edzés leírásának megadása kötelező!';
      isValid = false;
    }
    if (!this.form.image || this.form.image.trim() === '') {
      this.fieldErrors['Image'] = 'A kép megadása kötelező!';
      isValid = false;
    }

    if (!this.form.maxParticipant || this.form.maxParticipant < 1 || this.form.maxParticipant > 100) {
      this.fieldErrors['MaxParticipant'] = 'A résztvevők száma 1 és 100 között kell legyen!';
      isValid = false;
    }

    if (!this.form.startTime) {
      this.fieldErrors['StartTime'] = 'A kezdési idő megadása kötelező!';
      isValid = false;
    }
    if (!this.form.endTime) {
      this.fieldErrors['EndTime'] = 'A befejezési idő megadása kötelező!';
      isValid = false;
    }

    let startDateIso = '';
    let endDateIso = '';

    if (this.form.startTime && this.form.endTime) {
      const startDate = new Date(this.form.startTime);
      const endDate = new Date(this.form.endTime);
      const start = startDate.getTime();
      const end = endDate.getTime();
      const now = new Date().getTime();

      if (isNaN(start) || isNaN(end)) {
        this.fieldErrors['StartTime'] = 'Érvénytelen dátum!';
        isValid = false;
      } else {
        startDateIso = startDate.toISOString();
        endDateIso = endDate.toISOString();

        if (!this.editingId && start < (now - 60000)) {
          this.fieldErrors['StartTime'] = 'A kezdési idő nem lehet a múltban!';
          isValid = false;
        }

        if (end <= start) {
          this.fieldErrors['EndTime'] = 'A befejezésnek a kezdés után kell lennie!';
          isValid = false;
        } else {
          const durationMs = end - start;
          if (durationMs < 5 * 60 * 1000) {
            this.fieldErrors['EndTime'] = 'Az edzésnek legalább 5 percesnek kell lennie!';
            isValid = false;
          } else if (durationMs > 2 * 60 * 60 * 1000) {
            this.fieldErrors['EndTime'] = 'Az edzés legfeljebb 2 óra hosszú lehet!';
            isValid = false;
          }
        }
      }
    }

    const hasInvalidTickets = this.form.tickets.some(t => 
      !t.description || t.description.trim() === '' || t.price === null || t.price === undefined || t.price < 0
    );
    if (hasInvalidTickets) {
      this.ticketError = 'Minden jegyhez kötelező megadni a leírást, és az ár nem lehet negatív vagy üres!';
      isValid = false;
    }

    if (!isValid) {
      return;
    }

    this.isSaving = true;

    const dto: CreateTrainingDto = {
      ...this.form,
      maxParticipant: this.form.maxParticipant || 0,
      startTime: startDateIso, 
      endTime: endDateIso,
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

          if (this.fieldErrors['StartTime'] && this.fieldErrors['StartTime'].includes('előtt')) {
            this.fieldErrors['StartTime'] = 'A kezdési időpont nem lehet a múltban!';
          }
        } 
        else {
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

  isDeleting(id: number){
    return this.deletingIds.has(id);
  }

  getTimelineStartHour(){
    return 6;
  }

  getTimelineEndHour(){
    return 22;
  }

  getTimelineHours(){
    const hours: number[] = [];
    for (let i = this.getTimelineStartHour(); i <= this.getTimelineEndHour(); i += 2) {
      hours.push(i);
    }
    return hours;
  }

  getHourPosition(h: number){
    const totalHours = this.getTimelineEndHour() - this.getTimelineStartHour();
    return ((h - this.getTimelineStartHour()) / totalHours) * 100;
  }

  getBarLeft(startTime: string){
    const totalMinutes = (this.getTimelineEndHour() - this.getTimelineStartHour()) * 60;
    const d = new Date(startTime);
    const minutes = (d.getHours() - this.getTimelineStartHour()) * 60 + d.getMinutes();
    return Math.max(0, Math.min(100, (minutes / totalMinutes) * 100));
  }

  getBarWidth(startTime: string, endTime: string){
    const totalMinutes = (this.getTimelineEndHour() - this.getTimelineStartHour()) * 60;
    const start = new Date(startTime);
    const end = new Date(endTime);
    const duration = (end.getTime() - start.getTime()) / 60000;
    return Math.max(0.5, Math.min(100, (duration / totalMinutes) * 100));
  }

  getNewBarLeft(){
    if (!this.form.startTime) return 0;
    return this.getBarLeft(this.form.startTime);
  }

  getNewBarWidth(){
    if (!this.form.startTime || !this.form.endTime) return 0;
    const start = new Date(this.form.startTime).getTime();
    const end = new Date(this.form.endTime).getTime();
    if (isNaN(start) || isNaN(end) || end <= start) return 0;
    return this.getBarWidth(this.form.startTime, this.form.endTime);
  }

  hasOverlap(){
    if (!this.form.startTime || !this.form.endTime) return false;
    const newStart = new Date(this.form.startTime).getTime();
    const newEnd = new Date(this.form.endTime).getTime();
    return this.timelineTrainings.some(t => {
      const s = new Date(t.startTime).getTime();
      const e = new Date(t.endTime).getTime();
      return newStart < e && newEnd > s;
    });
  }

  isTodaySelected(){
    if (!this.form.startTime) return false;
    const selectedDate = new Date(this.form.startTime);
    const today = new Date();
    return selectedDate.getFullYear() === today.getFullYear() &&
           selectedDate.getMonth() === today.getMonth() &&
           selectedDate.getDate() === today.getDate();
  }

  getNowPosition(){
    const now = new Date();
    const totalMinutes = (this.getTimelineEndHour() - this.getTimelineStartHour()) * 60;
    const minutes = (now.getHours() - this.getTimelineStartHour()) * 60 + now.getMinutes();
    return Math.max(0, Math.min(100, (minutes / totalMinutes) * 100));
  }

  getTicketTypeLabel(type: number){
    switch (type) {
      case 0: return 'Edzésjegy';
      case 1: return 'Napi';
      case 2: return 'Havi';
      case 3: return 'Alkalomjegy';
      default: return '?';
    }
  }

  isUpcoming(t: TrainerTrainingModel){
    return new Date(t.endTime).getTime() > Date.now();
  }
  inProgress(t: TrainerTrainingModel){
    if (!t.startTime || !t.endTime) return false;
    
    const now = new Date().getTime();
    const start = new Date(t.startTime).getTime();
    const end = new Date(t.endTime).getTime();

    return start <= now && now < end;
  }

  viewTraining(id: number) {
    this.router.navigate(['/trainings', id]);
  }

  getTicketErrors(index: number){
    const prefix = `jegy.[${index}]`;
    return Object.entries(this.fieldErrors)
      .filter(([k]) => k.startsWith(prefix))
      .map(([, v]) => v);
  }

  hasAnyTicketErrors(){
    return Object.keys(this.fieldErrors).some(k => k.startsWith('jegy.'));
  }

  private toLocalDatetimeString(dateString: string){
    const d = new Date(dateString);

    const offset = d.getTimezoneOffset() * 60000; 
    const localISOTime = new Date(d.getTime() - offset).toISOString().slice(0, 16);

    return localISOTime;
  }

  private ensureUtc(dateString: string){
    if (!dateString) return dateString;

    const isoString = dateString.replace(' ', 'T');
    return isoString.endsWith('Z') ? isoString : isoString + 'Z';
  }
}
