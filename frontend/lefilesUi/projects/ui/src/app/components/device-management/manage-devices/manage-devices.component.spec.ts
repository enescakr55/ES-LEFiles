import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageDevicesComponent } from './manage-devices.component';

describe('ManageDevicesComponent', () => {
  let component: ManageDevicesComponent;
  let fixture: ComponentFixture<ManageDevicesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageDevicesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageDevicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
