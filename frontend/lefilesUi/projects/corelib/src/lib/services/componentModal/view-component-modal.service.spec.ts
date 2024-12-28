import { TestBed } from '@angular/core/testing';

import { ViewComponentModalService } from './view-component-modal.service';

describe('ViewComponentModalService', () => {
  let service: ViewComponentModalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ViewComponentModalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
