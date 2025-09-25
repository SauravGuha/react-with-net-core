

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfiguration;

namespace Persistence
{
    public class ActivityDbContext : DbContext
    {
        public ActivityDbContext(DbContextOptions<ActivityDbContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ActivityEntityConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}