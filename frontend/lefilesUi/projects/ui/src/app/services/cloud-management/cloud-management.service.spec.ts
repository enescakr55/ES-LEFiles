import { TestBed } from '@angular/core/testing';

import { CloudManagementService } from './cloud-management.service';

describe('CloudManagementService', () => {
  let service: CloudManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CloudManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
