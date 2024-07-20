import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { jwtDecode, JwtPayload } from 'jwt-decode';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'client';

  constructor() {
    // let token = ""
    let token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MCwibm9tZSI6IkpvaG4gRG9lIiwiZW1haWwiOiJqb2huQGdtYWlsLmNvbSIsInJvbGUiOiJhZG1pbiJ9.0y88aC-joyJ5LgkInDmS2yXV1Vo9ET6DYPe0lNlMNMQ"

    try {
      let decodedToken = jwtDecode<JwtPayload & {id: number; nome: string; email: string; role: string}>(token);
      console.log(decodedToken)
      console.log(decodedToken.id)
      console.log(decodedToken.nome)
      console.log(decodedToken.email)
      console.log(decodedToken.role)
    } catch (error) {
      console.log(error)
    }

    //document.cookie = "jwt-token="+ token;
    localStorage.setItem("jwt-token", token);
  }
}


