import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule  } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppService } from '../../service/app.service';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css'
})
export class LandingComponent {

  form: FormGroup;

  service = inject(AppService);
  formBuilder = inject(FormBuilder);
  router = inject(Router);

  constructor(private snackBar:MatSnackBar)
  {
    this.form = this.formBuilder.group({
      nome: [null],
      email: [null],
      password: [null]
    });
  }

  onSignUp() {
    this.service.signup(this.form.value).subscribe({
      next: (v) => {
        console.log(v),
        this.router.navigate(['/home']);
      },
      // error: (e) => console.error(e)
      error: (e) => this.snackBar.open(e.error , "⚠️", {duration:3000 }),
      complete: () => console.log('complete')
    });
  }

  onSignIn() {
    this.service.signin(this.form.value).subscribe({
      next: (v) => {
        console.log(v),
        this.router.navigate(['/home']);
      },
      // error: (e) => console.error(e)
      error: (e) => this.snackBar.open(e.error, "⚠️", {duration:3000 }),
      complete: () => console.log('complete')
    });
  }

}
