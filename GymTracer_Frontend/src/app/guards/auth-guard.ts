import { CanActivateFn, Router } from '@angular/router';
import { UserRole } from '../models/user.role.model';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (auth.actingUserRole === UserRole.not_found) {
    router.navigate(['/login'], {
      state: {
        message: 'A kért oldal megtekintéséhez be kell jelentkeznie.',
        type: 'error'
      }
    });
    return false;
  }
  return true;
};
