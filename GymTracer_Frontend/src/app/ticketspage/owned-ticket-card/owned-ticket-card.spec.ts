import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnedTicketCard } from './owned-ticket-card';

describe('OwnedTicketCard', () => {
  let component: OwnedTicketCard;
  let fixture: ComponentFixture<OwnedTicketCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OwnedTicketCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnedTicketCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
