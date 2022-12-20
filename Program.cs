using Microsoft.EntityFrameworkCore;
using PBL5BE.API.Data;
using PBL5BE.API.Services._User;
using PBL5BE.API.Services._UserInfo;
using PBL5BE.API.Services._Category;
using PBL5BE.API.Services._Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PBL5BE.API.Services._Product;
using PBL5BE.API.Services._Order;
using PBL5BE.API.Services._OrderDetail;
using PBL5BE.API.Services._Cart;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


var connectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.

//builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("192.168.1.24"), 7149));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseSentry();
ConfigureLogging();
builder.Host.UseSerilog();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myCORS",
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyMethod().AllowAnyHeader();
                      });
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "PBL6", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
// var serverVersion = new MySqlServerVersion(new Version(10, 4, 25));
// Replace 'YourDbContext' with the name of your own DbContext derived class.
services.AddDbContext<DataContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserInfoService, UserInfoService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddScoped<IProductService, ProductService>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IOrderDetailService, OrderDetailService>();
services.AddScoped<ICartService, CartService>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseCors("_myCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}