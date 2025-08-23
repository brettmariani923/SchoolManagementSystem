using Teachers.Application.Interfaces;
using Teachers.Application.Services;
using Teachers.Domain.Interfaces;
using Teachers.Domain.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
services.AddScoped<ICourseService, CourseDataService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
