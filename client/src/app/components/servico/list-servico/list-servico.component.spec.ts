import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListServicoComponent } from './list-servico.component';

describe('ListServicoComponent', () => {
  let component: ListServicoComponent;
  let fixture: ComponentFixture<ListServicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListServicoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListServicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
