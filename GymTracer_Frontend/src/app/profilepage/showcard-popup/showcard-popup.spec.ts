import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowcardPopup } from './showcard-popup';

describe('ShowcardPopup', () => {
  let component: ShowcardPopup;
  let fixture: ComponentFixture<ShowcardPopup>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShowcardPopup]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowcardPopup);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
