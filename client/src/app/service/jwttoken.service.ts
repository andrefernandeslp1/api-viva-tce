import { Injectable } from '@angular/core';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import {LocalStorageService} from "./local-storage.service";
// import {AppCookieService} from "./app-cookie.service";

@Injectable({
  providedIn: 'root'
})
export class JWTTokenService {

  jwtToken: string | null | undefined;
  decodedToken: JwtPayload & {id: number; nome: string; email: string; role: string} | undefined;

  constructor(private authStorageService: LocalStorageService)
  {
    this.jwtToken = this.authStorageService.get("jwt-token");
    if (this.jwtToken) {
      this.decodedToken = jwtDecode(this.jwtToken)
    }
  }

  getUser() {
    console.log(this.decodedToken);
    return this.decodedToken ? this.decodedToken.nome : null;
  }

  getEmail() {
    return this.decodedToken ? this.decodedToken.email : null;
  }

  getRole() {
    return this.decodedToken ? this.decodedToken.role : null;
  }

  getUserId() {
    return this.decodedToken ? this.decodedToken.id : null;
  }
}
