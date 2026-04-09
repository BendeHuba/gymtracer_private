import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ThemeService } from '../services/theme.service';

export const staffModeGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const theme = inject(ThemeService);
  const router = inject(Router);

  if (auth.pretendedUser) {
    router.navigate(['/profile']);
    return false;
  }

  if (!theme.isStaffMode) {
    router.navigate(['/']);
    return false;
  }

  return true;
};
