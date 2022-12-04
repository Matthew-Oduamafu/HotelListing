using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Repository;
using HotelListing.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// setting up the logger
builder.Logging.AddSerilog();

// setup dbContext
builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

builder.Services.AddResponseCaching();  // setting up caching
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentityServices();
builder.Services.ConfigureJWT(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
    {
        Duration = 120
    });
}).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
builder.Services.ConfigureVersioning();  // setting up API versioning

// setting up the cors policy
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// configure automapper
builder.Services.AddAutoMapper(typeof(MapperInitializer));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Listing API", Description = "API that provides list of hotles available" });
});

// logger configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
    path: @"E:\Codes\Code-Logs\hotel-listing-log-.txt",
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
        //app.UseSwagger();
        //app.UseSwaggerUI();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
    // configure custom error handler
    app.ConfigureExceptionHabdler();

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseResponseCaching();  // register middleware
    app.UseHttpCacheHeaders();  // register caching to middle ware
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    //app.MapControllers();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Failed to start");
}
finally
{
    Log.CloseAndFlush();
}