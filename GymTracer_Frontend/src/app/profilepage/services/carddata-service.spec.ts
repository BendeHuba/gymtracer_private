import { TestBed } from '@angular/core/testing';

import { CarddataService } from './carddata-service';

describe('CarddataService', () => {
  let service: CarddataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarddataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
