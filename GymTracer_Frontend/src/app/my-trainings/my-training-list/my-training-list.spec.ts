import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyTrainingList } from './my-training-list';

describe('MyTrainingList', () => {
  let component: MyTrainingList;
  let fixture: ComponentFixture<MyTrainingList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyTrainingList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyTrainingList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
