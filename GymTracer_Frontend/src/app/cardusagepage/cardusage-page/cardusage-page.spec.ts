import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardusagePage } from './cardusage-page';

describe('CardusagePage', () => {
  let component: CardusagePage;
  let fixture: ComponentFixture<CardusagePage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardusagePage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardusagePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
