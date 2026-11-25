using StudentManagementApi.Middleware;
using StudentManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Register services with the Dependency Injection (DI) container
builder.Services.AddControllers();
builder.Services.AddSingleton<IStudentService, StudentService>();

// Enable OpenAPI / Swagger UI for interactive endpoint testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. Configure the HTTP request processing pipeline (Middleware Order Matters)
// Global exception handler should wrap downstream middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// Log every incoming request and response duration
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map attribute-routed API controllers
app.MapControllers();

app.Run();
