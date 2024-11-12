import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileEntryItemComponent } from './file-entry-item.component';

describe('FileEntryItemComponent', () => {
  let component: FileEntryItemComponent;
  let fixture: ComponentFixture<FileEntryItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileEntryItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FileEntryItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
