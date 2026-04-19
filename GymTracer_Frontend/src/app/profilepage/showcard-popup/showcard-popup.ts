import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { CardDataModel } from '../userprofilemodels/carddata.model';
import { ThemeService } from '../../services/theme.service';
import { CarddataService } from '../services/carddata-service';
import { CommonModule } from '@angular/common';
import { QRCodeComponent } from 'angularx-qrcode';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-showcard-popup',
  imports: [CommonModule, QRCodeComponent],
  templateUrl: './showcard-popup.html',
  styleUrl: './showcard-popup.css',
})
export class ShowcardPopup {
  theme = inject(ThemeService);
  cardDataService = inject(CarddataService);
  authService = inject(AuthService);

  @Input() cardData!: CardDataModel;
  @Output() close = new EventEmitter<void>();

  deleteCard() {
    this.cardDataService.deleteCard(this.authService.actingUser!.id, this.cardData.id).subscribe({
      next: (res) => {
        this.close.emit();
      },
      error: (error) => {
        alert("Hiba történt a kártya törlése során! Kérem, próbálja újra később.");
        console.log(error.message);
      }
    });  
    
    
  }

  closeModal() {
    this.close.emit();
  }
}
