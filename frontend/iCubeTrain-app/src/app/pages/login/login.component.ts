import { Component, inject, OnInit } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../../services/auth.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterLink, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatInputModule, MatIconModule, ReactiveFormsModule, RouterLink, MatSnackBarModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  host: { class: 'w-full' },
})
export class LoginComponent implements OnInit{
  authService = inject(AuthService);
  matSnackbar = inject(MatSnackBar);
  router = inject(Router);
  hidePassword = true;
  loginForm!: FormGroup;
  fb = inject(FormBuilder);

  login() {
    this.authService.login(this.loginForm.value).subscribe({
      next: (response) => {
        this.matSnackbar.open('Login successful', 'Close', {
          duration: 2000,
          horizontalPosition: 'center',
        });
        this.router.navigate(['/']);
      },
      error: (error) => {
        this.matSnackbar.open('Login failed', 'Close', {
          duration: 2000,
          horizontalPosition: 'center',
        });
      },
    });
  }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
}
