import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingApplication } from './training-application';

describe('TrainingApplication', () => {
  let component: TrainingApplication;
  let fixture: ComponentFixture<TrainingApplication>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainingApplication]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainingApplication);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
