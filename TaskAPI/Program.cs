using Microsoft.EntityFrameworkCore;
using TaskAPI.Api.Middleware;
using TaskAPI.Core;
using TaskAPI.Core.Domain.RepositoryContracts;
using TaskAPI.Core.ServiceContracts;
using TaskAPI.Infrastructure;
using TaskAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString =
  builder.Configuration.GetConnectionString("TaskDb");
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(
  options =>
  {
    options.AddPolicy(
      myAllowSpecificOrigins,
      builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
  }
);

builder.Services.AddDbContext<ApplicationDbContext>(
  option => option.UseInMemoryDatabase(connectionString),
  ServiceLifetime.Singleton
);

builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<ITaskService, TaskService>();

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

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(myAllowSpecificOrigins);
app.MapControllers();

app.Run();
