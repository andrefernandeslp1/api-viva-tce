import { Usuario } from './../model/usuario';
import { inject, Injectable, signal } from '@angular/core';
import { JWTTokenService } from './jwttoken.service';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  jwtTokenService = inject(JWTTokenService);

  userLogged = signal<any>({
    id: this.jwtTokenService.getUserId(),
    nome: this.jwtTokenService.getUser(),
    email: this.jwtTokenService.getEmail(),
    role: this.jwtTokenService.getRole(),
  });

  public API_URL = 'http://localhost:3000';

  constructor(private httpClient: HttpClient) { }

  signup(usuario: Usuario): Observable<HttpResponse<any>>{
    return this.httpClient.post<any>(this.API_URL + "/signup", usuario, { observe: 'response' }).pipe(
      tap(response => {

        const token = response.body?.accessToken;
        if (token) {
          localStorage.setItem('jwt-token', token);
        }
        if(response.body.usuario.id) {
          localStorage.setItem('userId', response.body.usuario.id);
        }
        this.userLogged.set(response.body.usuario);
      })
    );
  }

  signin(usuario: Usuario): Observable<HttpResponse<any>> {
    return this.httpClient.post<any>(this.API_URL + "/signin", usuario, { observe: 'response' }).pipe(
      tap(response => {

        const token = response.body?.accessToken;
        if (token) {
          localStorage.setItem('jwt-token', token);
        }
        if(response.body.usuario.id) {
          localStorage.setItem('userId', response.body.usuario.id);
        }
        this.userLogged.set(response.body.usuario);
      })
    );
  }

}
