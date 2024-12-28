import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateFolderFromCloudComponent } from './update-folder-from-cloud.component';

describe('UpdateFolderFromCloudComponent', () => {
  let component: UpdateFolderFromCloudComponent;
  let fixture: ComponentFixture<UpdateFolderFromCloudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateFolderFromCloudComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateFolderFromCloudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
