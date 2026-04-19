import { TestBed } from '@angular/core/testing';

import { ScannerTrackerService } from './scanner.tracker.service';

describe('ScannerTrackerService', () => {
  let service: ScannerTrackerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScannerTrackerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
