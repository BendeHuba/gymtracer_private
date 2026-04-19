import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { userModeGuard } from './user-mode-guard';

describe('userModeGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => userModeGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});