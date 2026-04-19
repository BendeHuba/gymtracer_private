import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { staffModeGuard } from './staff-mode-guard';

describe('staffModeGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => staffModeGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});