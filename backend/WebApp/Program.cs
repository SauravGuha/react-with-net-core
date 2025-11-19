
using Api;
using Application;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddCors(options =>
        {
            var whiteListedSection = builder.Configuration.GetSection("WhiteListed");
            if (whiteListedSection != null)
            {
                var whiteListedUrls = whiteListedSection.Get<List<string>>()?.Select(e => e) ?? new List<string>();
                options.AddDefaultPolicy(po =>
                {
                    po.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(whiteListedUrls.ToArray())
                    .AllowCredentials();
                });
            }
        });
        builder.Services.AddAuthorization();
        builder.Services.AddIdentityApiEndpoints<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true; //when true we can use confirmEmail endpoint to confirm the user email
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ActivityDbContext>();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ExternalScheme;
        })
        .AddGitHub(options =>
        {
            options.ClientId = builder.Configuration.GetSection("Authentication:Github_ClientId")?.Value ?? "";
            options.ClientSecret = builder.Configuration.GetSection("Authentication:Github_ClientSecret")?.Value ?? "";
            options.CallbackPath = "api/ExternalAuth/github-callback";
            options.Scope.Add("user:email");
        })
        .AddIdentityCookies();
        builder.Services.AddHttpContextAccessor();

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

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGroup("api")
        .MapIdentityApi<User>();
        //Add api routes
        app.MapApiRoutes();
        app.MapFallbackToController("index", "FallBack");

        app.Services.InitialiseDb().GetAwaiter().GetResult();

        app.Run();
    }
}
