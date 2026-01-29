using CinemaTicketing.Api;
using CinemaTicketing.Api.Middlewares;
using CinemaTicketing.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog configured from appsettings.json
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));
builder.Services.AddOpenApi();

builder.Services.AddSignalR();



builder.Services.AddTransient<GlobalExceptionHandling>();

builder.Services
    .AddAuthentication(builder.Configuration)
    .AddCinemaApi(builder.Configuration);

var app = builder.Build();

// Optional but “standard”: request logging middleware
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseExercisesEndpoints();
    
app.UseMiddleware<GlobalExceptionHandling>();

app.UseHttpsRedirection();

app.UseCors(opt => opt.WithOrigins("http://127.0.0.1:5500")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CinemaContext>().Database;

    foreach (var migration in db.GetPendingMigrations())
    {
        Console.WriteLine($"Applying {migration}...");
    }
    
    await db.MigrateAsync();
}

app.Run();