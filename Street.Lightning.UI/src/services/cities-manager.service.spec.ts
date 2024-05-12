import { TestBed } from '@angular/core/testing';

import { CitiesManagerService } from './cities-manager.service';

describe('CitiesManagerService', () => {
  let service: CitiesManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CitiesManagerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
