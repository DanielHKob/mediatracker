import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMediaComponent } from './create-media.component';

describe('CreateMediaComponent', () => {
  let component: CreateMediaComponent;
  let fixture: ComponentFixture<CreateMediaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateMediaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateMediaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
