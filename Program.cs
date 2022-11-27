using HotelListing.Configurations;
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// setting up the logger
builder.Logging.AddSerilog();


// setup dbContext
builder.Services.AddDbContext<DatabaseContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

// Add services to the container.

builder.Services.AddControllers();


// setting up the cors policy
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b=> b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// configure automapper
builder.Services.AddAutoMapper(typeof(MapperInitializer));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Listing API", Description = "API that provides list of hotles available" });
});

// logger configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
    path: @"C:\Users\Innorik_5\OneDrive\Documents\Codes\MyLogs\hotel-listing-log-.txt",
    outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
    rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information
    ).CreateLogger();


try
{
    Log.Information("Application Is Starting");

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application Failed to start");
}
finally
{
    Log.CloseAndFlush();
}
