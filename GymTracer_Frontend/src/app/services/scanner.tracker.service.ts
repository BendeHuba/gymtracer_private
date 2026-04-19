import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ThemeService } from './theme.service';
import { AuthService } from './auth.service';
import { fromEvent, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ScannerTrackerService {
  router = inject(Router);
  theme = inject(ThemeService);
  auth = inject(AuthService);

  private buffer = '';
  private resetTimer?: number;
  private trackerSubscription?: Subscription;

  private guidRegex = /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/;

  startTracking(){
    this.trackerSubscription = fromEvent<KeyboardEvent>(document, 'keydown').subscribe({
      next: ($event) => {
        const target = $event.target as HTMLElement;
        if (target.tagName === 'INPUT' || target.tagName === 'TEXTAREA') return;
        
        let key = $event.key;
        if (key.length > 1) return;

        if (key.toLowerCase() === 'ö') key = '0';
        if (key.toLowerCase() === 'ü') key = '-';

        this.buffer += key;

        if (this.buffer.length >= 36) {
          const possibleGuid = this.buffer.slice(-36);
          
          if (this.guidRegex.test(possibleGuid)) {
            this.handleScan(possibleGuid);
          }
        }

        clearTimeout(this.resetTimer);
        this.resetTimer = setTimeout(() => {
          this.buffer = '';
        }, 1000);
      }
    })
  }

  private handleScan(guid: string){
    this.buffer = '';
    clearTimeout(this.resetTimer);

    if (this.theme.isStaffMode){
      this.router.navigate(['/users'], { queryParams: { guid: guid } });
    }
  }

  stopTracking(){
    this.trackerSubscription?.unsubscribe();
  }
}
