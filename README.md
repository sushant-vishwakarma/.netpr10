# Experiment 10: Student Management REST API (ASP.NET Core)

A clean ASP.NET Core 8.0 Web API demonstrating **Project Structure**, **Custom Middleware**, and **RESTful CRUD Endpoints**.

## Project Structure

```text
StudentManagementApi/
├── Controllers/
│   └── StudentsController.cs          # CRUD REST endpoints (/api/students)
├── Middleware/
│   ├── GlobalExceptionMiddleware.cs   # Centralized JSON error handling
│   └── RequestLoggingMiddleware.cs    # Request/response logging & X-Response-Time-Ms header
├── Models/
│   └── Student.cs                     # Student model with DataAnnotations validation
├── Services/
│   ├── IStudentService.cs             # Service contract
│   └── StudentService.cs              # Thread-safe in-memory data store
├── Properties/
│   └── launchSettings.json            # Local HTTP/HTTPS profiles & Swagger startup
├── Program.cs                         # DI registration & middleware pipeline
├── appsettings.json                   # Logging and host settings
├── StudentManagementApi.csproj        # .NET 8 Web SDK & Swashbuckle package
└── StudentManagementApi.http          # Ready-to-run HTTP test requests
```

## How to Run Locally

```bash
cd StudentManagementApi
dotnet restore
dotnet run
```

Open Swagger UI in your browser: `http://localhost:5000/swagger`

## Sample Endpoints to Screenshot for Lab Submission

1. `GET /api/students` — Lists all pre-seeded students (`200 OK`)
2. `POST /api/students` — Creates a new student (`201 Created`)
3. `PUT /api/students/1` — Updates student ID 1 (`200 OK`)
4. `DELETE /api/students/3` — Deletes student ID 3 (`204 No Content`)
5. `GET /api/students/error-test` — Shows structured JSON error from `GlobalExceptionMiddleware` (`500 Internal Server Error`)
6. Terminal console output — Shows `RequestLoggingMiddleware` logs with method, path, status code, and execution time in ms
