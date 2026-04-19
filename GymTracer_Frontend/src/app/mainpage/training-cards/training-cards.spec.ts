import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingCards } from './training-cards';

describe('TrainingCards', () => {
  let component: TrainingCards;
  let fixture: ComponentFixture<TrainingCards>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainingCards]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainingCards);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
