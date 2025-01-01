import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewThumbnailComponent } from './view-thumbnail.component';

describe('ViewThumbnailComponent', () => {
  let component: ViewThumbnailComponent;
  let fixture: ComponentFixture<ViewThumbnailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewThumbnailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewThumbnailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
