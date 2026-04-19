import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketsCard } from './tickets-card';

describe('TicketsCard', () => {
  let component: TicketsCard;
  let fixture: ComponentFixture<TicketsCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketsCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketsCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
