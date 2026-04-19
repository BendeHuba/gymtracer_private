import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileModificationForm } from './profile-modification-form';

describe('ProfileModificationForm', () => {
  let component: ProfileModificationForm;
  let fixture: ComponentFixture<ProfileModificationForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileModificationForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileModificationForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
