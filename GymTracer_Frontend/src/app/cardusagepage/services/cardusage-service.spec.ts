import { TestBed } from '@angular/core/testing';

import { CardusageService } from './cardusage-service';

describe('CardusageService', () => {
  let service: CardusageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CardusageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
