import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

export type Deployment = {
  id?: number;
  serviceName: string;
  version: string;
  environment: string;
  status: string;
  createdAtUtc?: string;
};

@Injectable({ providedIn: 'root' })
export class DeploymentsService {
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Deployment[]>(
      `${environment.apiBaseUrl}/api/deployments`
    );
  }

  create(dep: Deployment) {
    return this.http.post<Deployment>(
      `${environment.apiBaseUrl}/api/deployments`,
      dep
    );
  }
}