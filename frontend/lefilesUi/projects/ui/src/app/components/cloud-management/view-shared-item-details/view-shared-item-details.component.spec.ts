import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewSharedItemDetailsComponent } from './view-shared-item-details.component';

describe('ViewSharedItemDetailsComponent', () => {
  let component: ViewSharedItemDetailsComponent;
  let fixture: ComponentFixture<ViewSharedItemDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewSharedItemDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewSharedItemDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
