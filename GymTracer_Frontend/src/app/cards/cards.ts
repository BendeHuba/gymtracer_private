import { Component, inject, OnInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { ThemeService } from '../services/theme.service';
import { UserService } from '../services/user.service';
import { CardModel } from '../models/card.model';

@Component({
  selector: 'app-cards',
  standalone: true,
  imports: [NgClass],
  templateUrl: './cards.html',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class CardsPage implements OnInit {
  auth = inject(AuthService);
  theme = inject(ThemeService);
  userService = inject(UserService);

  isLoading = true;
  isCreating = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  cards: CardModel[] = [];
  deletingIds = new Set<number>();

  ngOnInit() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.loadCards(userId);
  }

  loadCards(id: number) {
    this.isLoading = true;
    this.errorMessage = null;
    this.userService.getCards(id).subscribe({
      next: (res) => {
        this.cards = res;
        this.isLoading = false;
      },
      error: (err) => {
        if (err.status === 404) {
          this.cards = [];
        } else {
          this.errorMessage = err.error?.error || 'Nem sikerült betölteni a kártyákat.';
        }
        this.isLoading = false;
      }
    });
  }

  createCard() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.isCreating = true;
    this.errorMessage = null;
    this.userService.createCard(userId).subscribe({
      next: (res) => {
        this.cards.push(...res);
        this.isCreating = false;
        this.successMessage = 'Kártya sikeresen létrehozva!';
        setTimeout(() => this.successMessage = null, 3000);
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Nem sikerült létrehozni a kártyát.';
        this.isCreating = false;
      }
    });
  }

  deleteCard(cardId: number) {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.deletingIds.add(cardId);
    this.userService.deleteCard(userId, cardId).subscribe({
      next: () => {
        this.cards = this.cards.filter(c => c.id !== cardId);
        this.deletingIds.delete(cardId);
        this.successMessage = 'Kártya sikeresen visszavonva!';
        setTimeout(() => this.successMessage = null, 3000);
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Nem sikerült visszavonni a kártyát.';
        this.deletingIds.delete(cardId);
      }
    });
  }

  isDeleting(cardId: number): boolean {
    return this.deletingIds.has(cardId);
  }
}
