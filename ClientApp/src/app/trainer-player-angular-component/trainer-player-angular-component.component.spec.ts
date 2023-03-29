import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainerPlayerAngularComponentComponent } from './trainer-player-angular-component.component';

describe('TrainerPlayerAngularComponentComponent', () => {
  let component: TrainerPlayerAngularComponentComponent;
  let fixture: ComponentFixture<TrainerPlayerAngularComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainerPlayerAngularComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainerPlayerAngularComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
