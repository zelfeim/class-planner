using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Features.Calendar.Services;
using Core.Features.Calendar.Services.Interfaces;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Logging.AddConsole();
builder.Logging.AddDebug();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddCors();
builder.Services.AddOpenApi();
builder
    .Services.AddAuthorization()
    .AddAuthenticationCookie(TimeSpan.FromHours(2))
    .AddFastEndpoints()
    .SwaggerDocument();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    o.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
});

builder.Services.AddScoped<ICalendarService, CalendarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(c =>
    {
        c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
        c.Serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .UseSwaggerGen();

app.Run();

public partial class Program { }
