

using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfiguration;

namespace Persistence
{
    public class ActivityDbContext : IdentityDbContext<User>
    {
        public ActivityDbContext(DbContextOptions<ActivityDbContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> ActivityUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ActivityEntityConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}