using Microsoft.EntityFrameworkCore;
using PBL5BE.API.Data;
using PBL5BE.API.Services._User;
using PBL5BE.API.Services._UserInfo;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


var connectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.
    
 builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("127.0.0.1"), 7149));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myCORS",
                      policy  =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyMethod().AllowAnyHeader();
                      });
});

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
// Replace 'YourDbContext' with the name of your own DbContext derived class.
services.AddDbContext<DataContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserInfoService, UserInfoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("_myCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();