using Teachers.Application.Interfaces;
using Teachers.Application.Services;
using Teachers.Domain.Implementation;
using Teachers.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Connection string (from User Secrets / env vars / appsettings.json placeholder)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register dependencies
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(connectionString));
builder.Services.AddScoped<IDataAccess, DataAccess>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
