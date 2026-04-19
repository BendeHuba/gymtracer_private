import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AllTrainingFilter, AllTrainingFilterLocal, AllTrainingResponse } from '../models/trainings.all.model';
import { ThemeService } from '../../services/theme.service';
import { TrainingService } from '../service/training.service';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TrainingItem } from "../components/training-item/training-item";
import { Router } from '@angular/router';

@Component({
  selector: 'app-trainings',
  imports: [NgClass, FormsModule, TrainingItem],
  templateUrl: './trainings.html',
  styleUrl: './trainings.css',
})
export class Trainings implements OnInit, OnDestroy {

  theme = inject(ThemeService);
  trainingService = inject(TrainingService);
  router = inject(Router);

  filters: AllTrainingFilterLocal = {
    keyword: '',
    start: '',
    end: '',
    trainerName: '',
    trainerId: undefined
  };

  trainings: AllTrainingResponse[] = [];
  
  isLoading: boolean = false;
  hasSearched: boolean = false;

  ngOnInit(){
    if(this.trainingService.lastFilters){
      const saved = this.trainingService.lastFilters;
      
      this.filters = {
        keyword: saved.keyword ?? '',
        start: saved.start ?? '',
        end: saved.end ?? '',
        trainerName: saved.trainerName ?? '',
        trainerId: saved.trainerId
      };

      this.trainings = this.trainingService.lastResults;
      this.hasSearched = this.trainingService.hasSearched;
    }
    else{
      this.setDefaultDates();
      this.loadTrainings();
    }
  }

  ngOnDestroy(){
    const destinationUrl = this.router.url;
    if (!destinationUrl.includes('/trainings/')) {
      this.trainingService.clearState();
    }
  }

  onSearch(){
    this.hasSearched = true;
    this.loadTrainings();
  }

  private setDefaultDates(){
    const today = new Date();
    
    const yyyy = today.getFullYear();
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const dd = String(today.getDate()).padStart(2, '0');
    
    this.filters.start = `${yyyy}-${mm}-${dd}T00:00`;
    this.filters.end = `${yyyy}-${mm}-${dd}T23:59`;
  }

  clearFilters(){
    this.filters = {
      keyword: '',
      start: '',
      end: '',
      trainerName: '',
      trainerId: undefined
    };
    this.setDefaultDates();
    this.trainings = [];
    this.hasSearched = false;
  }

  private loadTrainings(){
    this.isLoading = true;
    const activeFilters: AllTrainingFilter = {};
    
    if (this.filters.keyword?.trim()) activeFilters.keyword = this.filters.keyword.trim();
    if (this.filters.start) activeFilters.start = new Date(this.filters.start).toISOString();
    if (this.filters.end) activeFilters.end = new Date(this.filters.end).toISOString();
    if (this.filters.trainerName?.trim()) activeFilters.trainerName = this.filters.trainerName.trim();
    if (this.filters.trainerId) activeFilters.trainerId = this.filters.trainerId;

    this.trainingService.lastFilters = { ...this.filters };
    this.trainingService.hasSearched = this.hasSearched;

    this.trainingService.getAllTrainings(activeFilters).subscribe({
      next: (response) => {
        this.trainings = response.map(t => ({
          ...t,
          startTime: this.ensureUtc(t.startTime),
          endTime: this.ensureUtc(t.endTime)
        }));
        this.trainingService.lastResults = this.trainings;

        this.isLoading = false;
      },
      error: (error) => {
        console.error('Hiba történt a képzések betöltésekor:', error);
        this.isLoading = false;
      }
    });
  }

  private ensureUtc(dateString: string){
    if (!dateString) return dateString;

    const isoString = dateString.replace(' ', 'T');
    return isoString.endsWith('Z') ? isoString : isoString + 'Z';
  }
}
