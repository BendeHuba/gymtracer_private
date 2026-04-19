import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardsList } from './cards-list';

describe('CardsList', () => {
  let component: CardsList;
  let fixture: ComponentFixture<CardsList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardsList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardsList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
