import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainerComponentComponent } from './trainer-component.component';

describe('TrainerComponentComponent', () => {
  let component: TrainerComponentComponent;
  let fixture: ComponentFixture<TrainerComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainerComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainerComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
