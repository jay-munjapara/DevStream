import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { DeploymentsService, Deployment } from '../../services/deployments.service';
import { Subscription, interval } from 'rxjs';

@Component({
  selector: 'app-deployments',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './deployments.component.html'
})
export class DeploymentsComponent {
  private refreshSub?: Subscription;
  deployments: Deployment[] = [];
  error = '';

  form: Deployment = {
    serviceName: '',
    version: '',
    environment: 'dev',
    status: 'QUEUED'
  };

  constructor(
    private deploymentsService: DeploymentsService,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    // If no token, go to login
    if (!this.auth.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }
    this.load();
    this.refreshSub = interval(4000).subscribe(() => this.load());
  }
  ngOnDestroy() {
    this.refreshSub?.unsubscribe();
  }

  load() {
    this.error = '';
    this.deploymentsService.getAll().subscribe({
      next: (data) => this.deployments = data,
      error: () => this.error = 'Failed to load deployments. Are you logged in?'
    });
  }

  create() {
    this.error = '';
    this.deploymentsService.create(this.form).subscribe({
      next: () => {
        this.form = { serviceName: '', version: '', environment: 'dev', status: 'QUEUED' };
        this.load();
      },
      error: () => this.error = 'Failed to create deployment.'
    });
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}