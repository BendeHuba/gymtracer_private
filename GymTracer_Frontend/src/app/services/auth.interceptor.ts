import { HttpEvent, HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { tap } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);

  let clonedReq = req;

  if (auth.token) {
    clonedReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${auth.token}`)
    });
  }
    
  return next(clonedReq).pipe(
    tap({
      next: (event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          const sessionHeader = event.headers.get('session');
          
          if (sessionHeader) {
            try {
              const { validTo } = JSON.parse(sessionHeader);
              if (validTo) {
                auth.extendSession(validTo);
              }
            } catch {
              console.warn('Invalid session header', sessionHeader);
            }
          }
        }
      }
    })
  );
};
