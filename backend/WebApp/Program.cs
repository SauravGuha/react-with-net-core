
using Api;
using Application;
using Persistence;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApi();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(pb =>
            {
                pb.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            });
        });
        builder.Services.AddAuthentication()
        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration.GetSection("Issuer").Value;
            options.Audience = "reactivities-api";
            options.RequireHttpsMetadata = builder.Configuration.GetSection("Environment").Value == "Development";
        });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        if (app.Environment.IsDevelopment())
            app.UseHttpsRedirection();

        app.UseCors();
        app.UseException();

        app.UseAuthentication();
        app.UseAuthorization();

        //Add api routes
        app.MapApiRoutes();
        app.Services.InitialiseDb().GetAwaiter().GetResult();

        app.Run();
    }
}
