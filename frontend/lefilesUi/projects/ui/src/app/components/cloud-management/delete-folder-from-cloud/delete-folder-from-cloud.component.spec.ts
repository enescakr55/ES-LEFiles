import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteFolderFromCloudComponent } from './delete-folder-from-cloud.component';

describe('DeleteFolderFromCloudComponent', () => {
  let component: DeleteFolderFromCloudComponent;
  let fixture: ComponentFixture<DeleteFolderFromCloudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteFolderFromCloudComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteFolderFromCloudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
