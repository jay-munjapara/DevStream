import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'deployments', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'deployments',
    loadComponent: () => import('./pages/deployments/deployments.component').then(m => m.DeploymentsComponent)
  }
];