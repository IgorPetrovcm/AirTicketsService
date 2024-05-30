import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTicketsPageComponent } from './create-tickets-page.component';

describe('CreateTicketsPageComponent', () => {
  let component: CreateTicketsPageComponent;
  let fixture: ComponentFixture<CreateTicketsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateTicketsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateTicketsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
