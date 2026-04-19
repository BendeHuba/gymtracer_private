import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ThemeService } from '../../services/theme.service';

@Component({
  selector: 'app-onbehalf-banner',
  imports: [],
  templateUrl: './onbehalf-banner.html',
  styleUrl: './onbehalf-banner.css',
  host: { 
    'class': 'block -mb-4 z-10 relative' 
  }
})
export class OnbehalfBanner {
  auth = inject(AuthService);
  theme = inject(ThemeService);
  router = inject(Router);

  exit() {
    this.auth.pretendedUser = null;
    this.auth.removePretendedUser();
    this.router.navigate(['/users']);
  }
}
