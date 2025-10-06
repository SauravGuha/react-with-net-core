
using Api;
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
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(po =>
            {
                po.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
        });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseCors();

        app.UseAuthorization();

        //Add api routes
        app.MapApiRoutes();
        app.Services.InitialiseDb().GetAwaiter().GetResult();

        app.Run();
    }
}
