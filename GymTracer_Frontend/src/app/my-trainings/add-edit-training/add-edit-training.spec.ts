import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditTraining } from './add-edit-training';

describe('AddEditTraining', () => {
  let component: AddEditTraining;
  let fixture: ComponentFixture<AddEditTraining>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditTraining]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditTraining);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
