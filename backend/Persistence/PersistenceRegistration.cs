

using Domain.Models;
using Domain.Repositories.ActivityRepository;
using Domain.Repositories.AttendeeRespository;
using Domain.Repositories.PhotoRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository.ActivityRepository;
using Persistence.Repository.AttendeesRepo;
using Persistence.Repository.CommentRepository;
using Persistence.Repository.PhotoRepository;

namespace Persistence
{
    public static class PersistenceRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection sc, IConfiguration configuration)
        {
            sc.AddDbContext<ActivityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("default")));
            sc.AddScoped<IActivityQueryRepository, ActivityQueryRepository>();
            sc.AddScoped<IActivityCommandRepository, ActivityCommandRepository>();
            sc.AddScoped<IAttendeeCommandRepository, AttendeeCommandRepository>();
            sc.AddScoped<IAttendeeQueryRepository, AttendeeQueryRepository>();
            sc.AddScoped<IPhotoCommandRepository, PhotoCommandRepository>();
            sc.AddScoped<IPhotoQueryRepository, PhotoQueryRepository>();
            sc.AddScoped<ICommentCommandRepository, CommentCommandRepository>();
            sc.AddScoped<ICommentQueryRepository, CommentQueryRepository>();
            return sc;
        }

        public static async Task InitialiseDb(this IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ActivityDbContext>();
                var userMananger = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                await db.Database.MigrateAsync();

                if (!db.Users.Any())
                {
                    await userMananger.CreateAsync(new User
                    {
                        Id = "8e11ae52-8518-40ec-b70b-6011ca6edd56",
                        Email = "tom@yopmail.com",
                        DisplayName = "tom",
                        UserName = "tom@yopmail.com",
                        EmailConfirmed = true
                    }, "@Bcd.1234");
                    await userMananger.CreateAsync(new User
                    {
                        Id = "8e11ae52-8518-40ec-b70b-6011ca6edd63",
                        Email = "jerry@yopmail.com",
                        DisplayName = "jerry",
                        UserName = "jerry@yopmail.com",
                        EmailConfirmed = true
                    }, "@Bcd.1234");
                }

                if (!db.Activities.Any())
                {
                    db.Activities.Add(new Activity
                    {
                        Id = Guid.Parse("8b106180-41c5-45d2-bc97-68e1c9a469d9"),
                        Category = "Music",
                        City = "Las Vegas",
                        Description = "last Concert",
                        EventDate = DateTime.UtcNow.AddDays(15),
                        Title = "Linkin Park",
                        Venue = "Victoria",
                        Longitude = 20.0,
                        Latitude = 40.00,
                        IsCancelled = false,
                        Attendees = new[] {
                            new Attendees {
                            UserId ="8e11ae52-8518-40ec-b70b-6011ca6edd56",
                            ActivityId =Guid.Parse("8b106180-41c5-45d2-bc97-68e1c9a469d9"),
                            IsAttending = true, IsHost = true },
                            new Attendees {
                            UserId ="8e11ae52-8518-40ec-b70b-6011ca6edd63",
                            ActivityId =Guid.Parse("8b106180-41c5-45d2-bc97-68e1c9a469d9"),
                            IsAttending = true, IsHost = false } }
                    });
                    db.Activities.Add(new Activity
                    {
                        Id = Guid.Parse("8e11ae52-8518-40ec-b70b-6011ca6edd98"),
                        Category = "Music",
                        City = "Las Vegas",
                        Description = "Revival",
                        EventDate = DateTime.UtcNow.AddDays(35),
                        Title = "Evanescence",
                        Venue = "Victoria",
                        Longitude = 20.0,
                        Latitude = 40.00,
                        IsCancelled = false,
                        Attendees = new[] {
                            new Attendees {
                            UserId ="8e11ae52-8518-40ec-b70b-6011ca6edd56",
                            ActivityId =Guid.Parse("8e11ae52-8518-40ec-b70b-6011ca6edd98"),
                            IsAttending = true, IsHost = false },
                            new Attendees {
                            UserId ="8e11ae52-8518-40ec-b70b-6011ca6edd63",
                            ActivityId =Guid.Parse("8e11ae52-8518-40ec-b70b-6011ca6edd98"),
                            IsAttending = true, IsHost = true } }
                    });
                    await db.SaveChangesAsync(); ;
                }

            }
        }
    }
}