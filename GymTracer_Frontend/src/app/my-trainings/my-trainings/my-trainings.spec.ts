import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyTrainingsPage } from './my-trainings';

describe('MyTrainingsPage', () => {
  let component: MyTrainingsPage;
  let fixture: ComponentFixture<MyTrainingsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyTrainingsPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyTrainingsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
