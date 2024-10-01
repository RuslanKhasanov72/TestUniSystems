import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditbuildingComponent } from './editbuilding.component';

describe('EditbuildingComponent', () => {
  let component: EditbuildingComponent;
  let fixture: ComponentFixture<EditbuildingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditbuildingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditbuildingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
