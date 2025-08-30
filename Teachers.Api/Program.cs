using Microsoft.OpenApi.Models;
using Teachers.Application.Interfaces;
using Teachers.Application.Services;
using Teachers.Data.Implementation;
using Teachers.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(connectionString));
builder.Services.AddScoped<IDataAccess, DataAccess>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Teachers API",
        Version = "2.0",
        Description = "API for managing courses, teachers, students, and enrollments"
    });
});

var app = builder.Build();

// No-cache for Swagger UI & spec to prevent stale bundles
app.Use(async (ctx, next) =>
{
    if (ctx.Request.Path.StartsWithSegments("/docs2") || ctx.Request.Path.StartsWithSegments("/swagger"))
    {
        ctx.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
        ctx.Response.Headers["Pragma"] = "no-cache";
        ctx.Response.Headers["Expires"] = "0";
    }
    await next();
});

// OpenAPI JSON + built-in UI
app.UseSwagger(); // serves /swagger/v2/swagger.json
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "docs2"; // UI at /docs2 (new path to avoid old cache)
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Teachers API v2");
});

// CDN-based Swagger UI (fresh assets) at /docs-cdn
app.MapGet("/docs-cdn", () =>
{
    var html = """
    <!doctype html>
    <html>
      <head>
        <meta charset="utf-8"/>
        <title>Teachers API – Swagger UI (CDN)</title>
        <meta name="viewport" content="width=device-width, initial-scale=1"/>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swagger-ui-dist@5/swagger-ui.css"/>
        <style>body{margin:0} #ui{margin:0}</style>
      </head>
      <body>
        <div id="ui"></div>
        <script src="https://cdn.jsdelivr.net/npm/swagger-ui-dist@5/swagger-ui-bundle.min.js"></script>
        <script>
          window.ui = SwaggerUIBundle({
            url: '/swagger/v2/swagger.json',
            dom_id: '#ui'
          });
        </script>
      </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
