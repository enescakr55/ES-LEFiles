import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CloudManagementComponent } from './cloud-management.component';

describe('CloudManagementComponent', () => {
  let component: CloudManagementComponent;
  let fixture: ComponentFixture<CloudManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CloudManagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CloudManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
