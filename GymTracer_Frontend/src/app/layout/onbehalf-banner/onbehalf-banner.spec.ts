import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnbehalfBanner } from './onbehalf-banner';

describe('OnbehalfBanner', () => {
  let component: OnbehalfBanner;
  let fixture: ComponentFixture<OnbehalfBanner>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OnbehalfBanner]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OnbehalfBanner);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
