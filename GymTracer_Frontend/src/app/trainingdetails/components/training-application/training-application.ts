import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Ticket } from '../../models/training.ticket.model';
import { TrainingDetailService } from '../../service/training-detail.service';
import { Router } from '@angular/router';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'training-detail-application',
  imports: [DecimalPipe],
  templateUrl: './training-application.html',
  styleUrl: './training-application.css',
})
export class TrainingApplication {
  @Input({required: true}) trainingId!: number;
  @Input({required: true}) userId!: number;
  @Input({required: true}) tickets: Ticket[] = [];
  @Output() modalClose = new EventEmitter();

  trainingDetailService = inject(TrainingDetailService);
  router = inject(Router);

  isSubmitting: boolean = false;
  selectedTicketId: number | null = null;

  private scrollPosition = 0;

  private preventWheel = (e: Event) => {
    const target = e.target as HTMLElement;

    if (target.closest('.ticket-scroll-container')) {
      return; 
    }
    e.preventDefault();
  };

  private lockScrollPosition = () => {
    window.scrollTo(0, this.scrollPosition);
  };

  ngOnInit() {
    this.scrollPosition = window.scrollY;

    window.addEventListener('wheel', this.preventWheel, { passive: false });
    window.addEventListener('touchmove', this.preventWheel, { passive: false });

    window.addEventListener('scroll', this.lockScrollPosition);
  }

  ngOnDestroy() {
    window.removeEventListener('wheel', this.preventWheel);
    window.removeEventListener('touchmove', this.preventWheel);
    window.removeEventListener('scroll', this.lockScrollPosition);
  }

  selectTicket(ticketId: number) {
    this.selectedTicketId = ticketId;
  }

  closeModal(){
    this.modalClose.emit();
  }

  onSubmitApplication() {
    if (!this.selectedTicketId || !this.userId || !this.trainingId) return;

    this.isSubmitting = true;

    this.trainingDetailService.applyToTraining(this.userId, this.trainingId, this.selectedTicketId)
      .subscribe({
        next: (res) => {
          this.isSubmitting = false;
          this.closeModal();
          
          this.router.navigate(['/user',this.userId,'tickets']);
        },
        error: (err) => {
          this.isSubmitting = false;
          console.error(err);
          alert(err.error?.message || 'Hiba történt a jelentkezés során.');
        }
      });
  }
}
