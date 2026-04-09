import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingItem } from './training-item';

describe('TrainingItem', () => {
  let component: TrainingItem;
  let fixture: ComponentFixture<TrainingItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainingItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainingItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
