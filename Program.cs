using Microsoft.EntityFrameworkCore;
using PBL5BE.API.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myCORS",
                      policy  =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyMethod().AllowAnyHeader();
                      });
});




var connectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
// Replace 'YourDbContext' with the name of your own DbContext derived class.
services.AddDbContext<DataContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("_myCORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();