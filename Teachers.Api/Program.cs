using Teachers.Application.Interfaces;
using Teachers.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddScoped<ICourseService, CourseService>();

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
