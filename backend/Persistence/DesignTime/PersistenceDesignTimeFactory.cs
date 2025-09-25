

using Domain.Models;
using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    namespace Persistence.DesignTime
    {
        public class PersistenceDesignTimeFactory : IDesignTimeDbContextFactory<ActivityDbContext>
        {
            public ActivityDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ActivityDbContext>();
                builder.UseSqlServer("");
                var context = new ActivityDbContext(builder.Options);
                return context;
            }
        }
    }