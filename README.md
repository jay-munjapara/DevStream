# DevStream

Full-Stack Deployment and Monitoring Portal

# Overview

DevStream is a full-stack deployment tracking and monitoring portal built using ASP.NET Core, Angular, SQL Server, and Docker.

The application simulates a CI/CD-style deployment workflow, enabling authenticated users to create deployments and monitor lifecycle transitions in a secure and scalable architecture.

This project demonstrates end-to-end system design including authentication, persistence, background processing, monitoring endpoints, and containerized infrastructure.

# Architecture

The system follows a standard three-tier architecture:

Angular UI (Frontend)
→ ASP.NET Core Web API (Backend)
→ SQL Server (Persistence Layer)

Authentication is handled via JWT (Bearer tokens).
Data access is implemented using Entity Framework Core.
Infrastructure is containerized using Docker.

# Technology Stack
Backend
 - ASP.NET Core Web API
 - Entity Framework Core
 - SQL Server
 - JWT Authentication
 - CORS configuration
 - Background hosted services
 - Health monitoring endpoints

Frontend
 - Angular (Standalone components)
 - HttpClient
 - JWT HTTP Interceptor
 - Component-based UI architecture

Infrastructure
 - Docker
 - Docker Compose
 - GitHub
 - CI/CD ready structure

# Project Structure
```
DevStream/

backend/
└── DevStream.API/
    ├── Controllers/
    │   ├── AuthController.cs
    │   └── DeploymentsController.cs
    │
    ├── Data/
    │   └── AppDbContext.cs
    │
    ├── Models/
    │   └── Deployment.cs
    │
    ├── Services/
    │   └── DeploymentWorker.cs
    │
    ├── Properties/
    │   └── launchSettings.json
    │
    ├── Program.cs
    ├── appsettings.json
    └── DevStream.API.csproj

frontend/
└── devstream-ui/
    ├── src/app/
    │   ├── pages/
    │   │   ├── login/
    │   │   └── deployments/
    │   │
    │   ├── services/
    │   │   ├── auth.service.ts
    │   │   └── deployments.service.ts
    │   │
    │   ├── interceptors/
    │   │   └── auth.interceptor.ts
    │   │
    │   ├── app.routes.ts
    │   └── app.config.ts
    │
    └── environments/

docker-compose.yml
README.md
```

# Authentication Flow

1. User submits credentials from Angular UI.

2. UI sends POST request to /api/auth/login.

3. Backend validates credentials.

4. A signed JWT token is generated.

5. Angular stores the token in local storage.

6. HTTP interceptor attaches Authorization: Bearer <token> to protected requests.

This enables stateless authentication between frontend and backend.

# Deployment Lifecycle Flow

1. User creates a deployment record via the dashboard.

2. Deployment is stored in SQL Server with initial status QUEUED.

3. A background worker service runs periodically.

4. Status transitions automatically:

  - QUEUED → RUNNING

  - RUNNING → SUCCESS or FAILED

5. Angular dashboard auto-refreshes to reflect updated state.

This simulates real CI/CD pipeline behavior.

# API Endpoints
**Authentication**

POST ```/api/auth/login```

**Deployments**

GET ```/api/deployments```
POST ```/api/deployments```

**Monitoring**

GET ```/```
GET ```/health```
GET ```/healthz```

# Running the Application Locally
1. Start SQL Server (Docker)
```docker compose up -d```
2. Run Backend
```
cd backend/DevStream.API
dotnet run --urls "http://127.0.0.1:5276"
```

Swagger UI:

```http://127.0.0.1:5276/swagger
```
3. Run Frontend
```
cd frontend/devstream-ui
ng serve
```

Frontend UI:

```http://localhost:4200```
Running Full Stack with Docker
```docker compose up -d --build```

Services:

UI: http://localhost:4200

API: http://localhost:8080

Swagger: http://localhost:8080/swagger

SQL Server: localhost:1433

# Design Considerations

 - Stateless authentication using JWT.

 - Separation of concerns across controllers, services, and data layer.

 - EF Core migrations for schema management.

 - Background service to simulate asynchronous deployment processing.

 - Dockerized database for environment consistency.

 - Health endpoints for readiness and monitoring support.

# Future Enhancements

 - Role-based authorization

 - Deployment audit logs

 - Real CI/CD webhook integration

 - Metrics export (Prometheus compatible)

 - Cloud deployment (AWS EC2 / ECS)

 - API rate limiting

 - Structured centralized logging

# Purpose

This project demonstrates:

 - Full-stack application design

 - Secure API development

 - Background job processing

 - Containerized infrastructure

 - Realistic deployment workflow modeling

It reflects patterns commonly used in enterprise software engineering environments.
