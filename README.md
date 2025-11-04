# Pro Tasker - Team-Based Task Management & Authorization System

**Pro Tasker** is a team-based task management API with **role-based authorization**, built using **ASP.NET Core**, **Entity Framework Core**, and **JWT authentication**.  
It is designed for small-scale team collaboration, where each user belongs to a single team.

---

## Project Roadmap / Features

1. **CRUD Operations** [Done] (Completed)  
   Full Create, Read, Update, Delete functionality for all main entities.

2. 4. **Layered Architecture** [Done] (Implemented)  
   Domain-Driven, multi-layer structure with clean separation between API, Application, Domain, and Infrastructure layers.

3. **Identity + JWT Integration** [Done] (Completed)  
   Secure user authentication and JWT token generation.

4. **Role-Based Authorization** [Done] (Completed)  
   Role-based access for Admin, TeamLeader, and Member.

5. **AutoMapper Integration**  [Done] (Completed)
   Simplifies entity-to-DTO mapping.

6. **Entity Relationships & EF Configurations**  [Done] (Completed)
   Well-structured one-to-many relationships with proper database configuration and seeding.

7. **Validation (FluentValidation)**  
   Ensures accurate and secure request data.

8. **Exception Handling & Logging**  
   Centralized error handling and structured logging.

9. **Unit Testing**  
   Service and controller test coverage.

10. **Logging**   
   Centralized structured logging setup.

11. **GitHub & README**  
   Version control and documentation setup.

12. **Full Documentation**  
   Endpoint references and usage guides.

---

## Entity Relationship Summary (One-to-Many)

Each **user** belongs to a single **team**,  
each **team** can manage multiple **members** and **tasks**.  
This approach is optimized for small-scale collaboration platforms.

| Relationship                 | Type | Description                                         |
|------------------------------|------|-----------------------------------------------------|
| Team -> Users                | 1:N  | A team can have multiple users.                     |
| User -> Team                 | N:1  | Each user belongs to only one team.                 |
| Team -> Leader               | 1:1  | Each team has one leader (User reference).          |
| Team -> ProjectTasks         | 1:N  | A team can have multiple project tasks.             |
| User -> ProjectTasks         | 1:N  | A user can be assigned multiple project tasks.      |
| ProjectTask -> TaskHistories | 1:N  | Each task can have multiple history records.        |
| User -> TaskHistories        | 1:N  | Users perform actions that generate task histories. |

---

## Folder Structure

ProTasker.Api
|-- Controllers
|   |-- AuthController.cs
|   |-- UsersController.cs
|   |-- TeamsController.cs
|   |-- ProjectTasksController.cs
|   `-- TaskHistoriesController.cs
|-- Extensions
`-- MiddleWares

ProTasker.Application
|-- DTOs
|-- Exceptions
|-- Helpers
|-- Interfaces
|   |-- Repositories
|   `-- Services
|-- Mapping
|-- Models
|-- Services
`-- Validators

ProTasker.Domain
|-- Entities
`-- Enums

ProTasker.Infrastructure
|-- Configurations
|-- Data
|-- Migrations
`-- Repositories

---

## Technologies Used

- **ASP.NET Core 8** – API Framework  
- **Entity Framework Core** - ORM for data access  
- **SQL Server** - Relational database  
- **AutoMapper** - DTO & Entity mapping  
- **JWT Authentication** - Secure login  
- **Role-based Authorization** - Admin, TeamLeader, Member  
- **FluentValidation** - Data validation  
- **Serilog / Logging** - Centralized structured logging *(planned)*  
- **Exception Handling** - *(planned)*  
- **Unit Testing** - *(planned)*  

---

## API Endpoints Overview

### Users
- `GET /api/users` – Get all users (Admin/TeamLeader)  
- `GET /api/users/{id}` – Get user by ID  
- `POST /api/users` – Create new user (Admin)  
- `PUT /api/users/{id}` – Update user (Admin/TeamLeader)  
- `DELETE /api/users/{id}` – Delete user (Admin)  

### Teams
- CRUD operations for managing teams  
- Each team has a leader and multiple members  

### Project Tasks
- CRUD endpoints for managing project tasks  
- Linked to both teams and assigned users  

### Task Histories
- CRUD endpoints for tracking task changes  
- Stores action history for each task  

### Authentication
- `POST /api/auth/login` – Login and get JWT token  
- `POST /api/auth/register` – Register new user  

---

## Getting Started

### 1. Clone the repository
```bash
git clone <repository-url>
   ```
### 2. Restore NuGet Packages
To restore all dependencies and NuGet packages, run:
```bash
dotnet restore
```
## 3. Apply Migrations and Seed the Database

Apply Entity Framework Core migrations to create and seed the database:

```bash
dotnet ef database update
```
## 4. Run the API

Start the application:

```bash
dotnet run
```
## 5. Access Swagger UI

Once the API is running, open your browser and visit:

https://localhost:<port>/swagger/index.html

This will open the Swagger UI, where you can test all endpoints interactively.

## Developer

**Aliye Bulut**  
*Junior Backend Developer*

 [LinkedIn](https://www.linkedin.com/in/aliye-bulut-phd-867453357/)  
 [GitHub](https://github.com/AlyBlt)


