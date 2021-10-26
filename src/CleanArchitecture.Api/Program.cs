using CleanArchitecture.Api.Infrastructure.Extensions;
using CleanArchitecture.Api.Infrastructure.Middleware;
using CleanArchitecture.Api.Routes;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseErrorHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapRouteHandler<TodoListRoutes>();
app.MapRouteHandler<TodoItemRoutes>();

app.UseHealthChecks("/health");

builder.MigrateDatabase();

app.Run();