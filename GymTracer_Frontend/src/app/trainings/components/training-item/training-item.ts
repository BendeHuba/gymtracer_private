import { Component, inject, Input } from '@angular/core';
import { AllTrainingResponse } from '../../models/trainings.all.model';
import { DatePipe } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { TrainingService } from '../../service/training.service';

@Component({
  selector: 'trainings-training-item',
  imports: [DatePipe, RouterLink],
  templateUrl: './training-item.html',
  styleUrl: './training-item.css',
})
export class TrainingItem {
  @Input({required: true}) training!: AllTrainingResponse;

  trainingService = inject(TrainingService);
  
  onImgError(event: Event) {
    const img = event.target as HTMLImageElement;
    img.src = 'images/training_default.jpg';
  }
  preserveListState(){
    this.trainingService.preserveState = true;
  }
}
