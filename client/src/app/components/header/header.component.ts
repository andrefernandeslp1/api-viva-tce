import { Component, inject, signal, WritableSignal } from '@angular/core';
import { AppService } from '../../service/app.service';
import { RouterModule } from '@angular/router';
import { Usuario } from '../../model/usuario';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  appService = inject(AppService);

  usuario!: WritableSignal<Usuario>;
  home!: WritableSignal<string>;

  constructor() {
    this.usuario = this.appService.userLogged;
    this.home = this.appService.home;
  }

  ngOnInit(): void {
    console.log(this.usuario().email);
  }

  imprimir(): void {
    console.log(this.usuario().id);
  }

}
