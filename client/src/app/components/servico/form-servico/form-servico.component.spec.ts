import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormServicoComponent } from './form-servico.component';

describe('FormServicoComponent', () => {
  let component: FormServicoComponent;
  let fixture: ComponentFixture<FormServicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormServicoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FormServicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
