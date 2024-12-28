import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessWatcherComponent } from './process-watcher.component';

describe('ProcessWatcherComponent', () => {
  let component: ProcessWatcherComponent;
  let fixture: ComponentFixture<ProcessWatcherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcessWatcherComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProcessWatcherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
