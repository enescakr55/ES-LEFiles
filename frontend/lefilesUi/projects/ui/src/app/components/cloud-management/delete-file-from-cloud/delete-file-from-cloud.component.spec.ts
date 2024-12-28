import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteFileFromCloudComponent } from './delete-file-from-cloud.component';

describe('DeleteFileFromCloudComponent', () => {
  let component: DeleteFileFromCloudComponent;
  let fixture: ComponentFixture<DeleteFileFromCloudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteFileFromCloudComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteFileFromCloudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
