

using Domain.Models;
using Domain.Repositories.ActivityRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository.ActivityRepository;

namespace Persistence
{
    public static class PersistenceRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection sc, IConfiguration configuration)
        {
            sc.AddDbContext<ActivityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("default")));
            sc.AddScoped<IActivityQueryRepository, ActivityQueryRepository>();
            sc.AddScoped<IActivityCommandRepository, ActivityCommandRepository>();
            return sc;
        }

        public static async Task InitialiseDb(this IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ActivityDbContext>();
                var userMananger = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                await db.Database.MigrateAsync();

                if (!db.Activities.Any())
                {
                    db.Activities.Add(new Activity
                    {
                        Category = "Music",
                        City = "Las Vegas",
                        Description = "last Concert",
                        EventDate = DateTime.UtcNow.AddDays(15),
                        Title = "Linkin Park",
                        Venue = "Victoria",
                        Longitude = 20.0,
                        Latitude = 40.00,
                        IsCancelled = false
                    });
                    await db.SaveChangesAsync(); ;
                }

                if (!db.Users.Any())
                {
                    await userMananger.CreateAsync(new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "tom@yopmail.com",
                        DisplayName = "tom",
                        UserName = "tom@yopmail.com",
                        EmailConfirmed = true
                    }, "@Bcd.1234");
                    await userMananger.CreateAsync(new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "jerry@yopmail.com",
                        DisplayName = "jerry",
                        UserName = "jerry@yopmail.com",
                        EmailConfirmed = true
                    }, "@Bcd.1234");
                }

            }
        }
    }
}