import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DatePipe } from '@angular/common';
import { TrainerTrainingModel } from '../../models/trainer-training.model';

@Component({
  selector: 'app-my-training-list',
  imports: [DatePipe],
  templateUrl: './my-training-list.html',
  styleUrl: './my-training-list.css'
})
export class MyTrainingList {
  @Input() trainings: TrainerTrainingModel[] = [];
  @Input() isLoading = false;
  @Input() errorMessage: string | null = null;
  @Input() successMessage: string | null = null;
  @Input() deletingIds = new Set<number>();

  @Output() create = new EventEmitter<void>();
  @Output() edit = new EventEmitter<TrainerTrainingModel>();
  @Output() delete = new EventEmitter<number>();
  @Output() view = new EventEmitter<number>();

  isDeleting(id: number) {
    return this.deletingIds.has(id);
  }

  isUpcoming(t: TrainerTrainingModel) {
    return new Date(t.endTime).getTime() > Date.now();
  }

  inProgress(t: TrainerTrainingModel) {
    if (!t.startTime || !t.endTime) return false;
    const now = new Date().getTime();
    const start = new Date(t.startTime).getTime();
    const end = new Date(t.endTime).getTime();
    return start <= now && now < end;
  }
}
