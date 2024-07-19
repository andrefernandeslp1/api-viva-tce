import { Component, inject, signal, WritableSignal } from '@angular/core';
import { AppService } from '../../service/app.service';
import { RouterModule } from '@angular/router';
import { Usuario } from '../../model/usuario';
import { JWTTokenService } from '../../service/jwttoken.service';

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
  jwtService = inject(JWTTokenService);

  usuario!: WritableSignal<Usuario>;

  constructor() {
    this.usuario = this.appService.userLogged;
  }

  ngOnInit(): void {

  }

  logout(): void {
    localStorage.clear();
    this.usuario.set({} as Usuario);
    console.log(this.usuario());
    this.jwtService.decodedToken = undefined;
  }
}
