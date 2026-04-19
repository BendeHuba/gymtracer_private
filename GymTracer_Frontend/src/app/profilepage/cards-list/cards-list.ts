import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { CarddataService } from '../services/carddata-service';
import { AuthService } from '../../services/auth.service';
import { CardDataModel } from '../userprofilemodels/carddata.model';
import { ThemeService } from '../../services/theme.service';
import { DatePipe, NgClass } from '@angular/common';
import { ShowcardPopup } from '../showcard-popup/showcard-popup';

@Component({
  selector: 'app-cards-list',
  imports: [NgClass, DatePipe, ShowcardPopup],
  templateUrl: './cards-list.html',
  styleUrl: './cards-list.css',
})
export class CardsList implements OnInit {

  theme = inject(ThemeService);
  carddataService = inject(CarddataService);
  authService = inject(AuthService);
  cardIsBeingShown = false;

  @Input() cardsOfUser: CardDataModel[] = [];
  @Output() cardsOfUserChange = new EventEmitter<CardDataModel[]>
  selectedCard!: CardDataModel | null;

  ngOnInit(): void {
    this.getCards();
  }

  getCards() {
    return this.carddataService.getCardsOfUser(this.authService.actingUser!.id).subscribe({
      next: (res) => {
        res.forEach((card) => {
          const utcDate = card.createdAt + 'Z';
          card.createdAt = new Date(utcDate);
        });
        this.cardsOfUser = res;
        this.cardsOfUserChange.emit(res);
      },
      error: (error) => {
        this.cardsOfUser = [];
        this.cardsOfUserChange.emit([]);
      }
    });
  }

  showCardDetails(card: CardDataModel) {
    this.selectedCard = card;
    this.cardIsBeingShown = true;
  }

  closeCardDetails() {
    this.selectedCard = null;
    this.cardIsBeingShown = false;
    this.getCards();
  }

  getNewCard() {
    this.carddataService.requestNewCard(this.authService.actingUser!.id).subscribe({
      next: (res) => {
        this.getCards();
      },
      error: (error) => {
        console.log(error.message);
      }
    });
  }
}
