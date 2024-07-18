import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesServicoComponent } from './detalhes-servico.component';

describe('DetalhesServicoComponent', () => {
  let component: DetalhesServicoComponent;
  let fixture: ComponentFixture<DetalhesServicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesServicoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DetalhesServicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
