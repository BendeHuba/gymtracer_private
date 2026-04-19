import { TestBed } from '@angular/core/testing';

import { Usertickets } from './usertickets';

describe('Usertickets', () => {
  let service: Usertickets;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Usertickets);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
