import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  email = 'admin@devstream.com';
  password = 'Admin123!';
  error = '';

  constructor(private auth: AuthService, private router: Router) {}

  onLogin() {
    this.error = '';
    this.auth.login(this.email, this.password).subscribe({
      next: () => this.router.navigate(['/deployments']),
      // error: () => this.error = 'Invalid credentials. Try admin@devstream.com / Admin123!'
      error: (err) => {
        console.log('Login error:', err);
        this.error = err?.error?.message
          ? `Login failed: ${err.error.message}`
          : `Login failed (status ${err.status}). Check API URL/CORS/backend.`;
      }
    });
  }
}