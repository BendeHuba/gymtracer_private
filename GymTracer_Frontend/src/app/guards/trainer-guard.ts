import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserRole } from '../models/user.role.model';

export const trainerGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const role = auth.user?.role;
  if (role === UserRole.trainer || role === UserRole.staff || role === UserRole.admin) {
    return true;
  }

  router.navigate(['/']);
  return false;
};
