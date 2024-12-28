import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateFolderToCloudComponent } from './create-folder-to-cloud.component';

describe('CreateFolderToCloudComponent', () => {
  let component: CreateFolderToCloudComponent;
  let fixture: ComponentFixture<CreateFolderToCloudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateFolderToCloudComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateFolderToCloudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
