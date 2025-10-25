
using Api;
using Application;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
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
            var whiteListedSection = builder.Configuration.GetSection("WhiteListed");
            if (whiteListedSection != null)
            {
                options.AddDefaultPolicy(po =>
                {
                    po.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(whiteListedSection.Get<List<string>>()!.Select(e => e).ToArray())
                    .AllowCredentials();
                });
            }
        });
        builder.Services.AddAuthorization();
        builder.Services.AddIdentityApiEndpoints<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ActivityDbContext>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddInfrastructure();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        //Add api routes
        app.MapGroup("api")
        .MapIdentityApi<User>();
        app.MapApiRoutes();
        app.Services.InitialiseDb().GetAwaiter().GetResult();

        app.Run();
    }
}
