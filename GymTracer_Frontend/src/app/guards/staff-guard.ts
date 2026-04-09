import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserRole } from '../models/user.role.model';
import { ThemeService } from '../services/theme.service';

export const staffGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const theme = inject(ThemeService);
  const router = inject(Router);

  if (auth.user?.role === UserRole.staff || auth.user?.role === UserRole.admin) {
    return true;
  }

  router.navigate(['/']);
  return false;
};
