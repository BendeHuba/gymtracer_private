import { TestBed } from '@angular/core/testing';

import { IncomestatService } from './incomestat-service';

describe('IncomestatService', () => {
  let service: IncomestatService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IncomestatService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
