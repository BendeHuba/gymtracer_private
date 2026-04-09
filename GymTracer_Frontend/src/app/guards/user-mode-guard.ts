import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ThemeService } from '../services/theme.service';

export const userModeGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const theme = inject(ThemeService);
  const router = inject(Router);

  if (theme.isStaffMode && !auth.pretendedUser) {
    router.navigate(['/users']);
    return false;
  }

  return true;
};
