import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ThemeService } from '../../services/theme.service';

@Component({
  selector: 'app-onbehalf-banner',
  standalone: true,
  imports: [],
  templateUrl: './onbehalf-banner.html',
})
export class OnbehalfBanner {
  auth = inject(AuthService);
  theme = inject(ThemeService);
  router = inject(Router);

  exit() {
    this.auth.pretendedUser = null;
    this.theme.isPretendMode = false;
    localStorage.removeItem('pretended_user');
    this.router.navigate(['/users']);
  }
}
