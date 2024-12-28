import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CloudFileUploadComponent } from './cloud-file-upload.component';

describe('CloudFileUploadComponent', () => {
  let component: CloudFileUploadComponent;
  let fixture: ComponentFixture<CloudFileUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CloudFileUploadComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CloudFileUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
