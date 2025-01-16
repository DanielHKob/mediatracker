import { TestBed } from '@angular/core/testing';

import { MediaitemsService } from './mediaitems.service';

describe('MediaitemsService', () => {
  let service: MediaitemsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MediaitemsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
