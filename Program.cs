using WebApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using WebApi.Extensao;
using WebApi.Filters;
using WebApi.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("AppDbContext")
        ?? throw new InvalidOperationException("Connection string não encontrada");

    options.UseNpgsql(connectionString)
           .UseSnakeCaseNamingConvention();
});

// SERIALIZAÇÃO DO JSON -> REFERENCIA
builder.Services.AddControllers().AddJsonOptions(Options =>
    Options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ApiLoggingFilter>();

builder.Logging.AddProvider(new CustomerLoggerProvider(new CustomLoggerProviderConfiguration
{
    logLevel = LogLevel.Information

}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "API"));

    app.ConfigureExceptionHandler();

}

app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
