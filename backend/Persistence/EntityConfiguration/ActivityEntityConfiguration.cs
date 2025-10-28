

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    public class ActivityEntityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable(nameof(Activity));

            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.EventDate);

            builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(e => e.UpdatedAt)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(e => e.EventDate)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(e => e.Title)
            .HasMaxLength(200)
            .IsRequired();

            builder.Property(e => e.Description)
            .HasMaxLength(500)
            .IsRequired();

            builder.Property(e => e.Category)
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(e => e.City)
            .HasMaxLength(500)
            .IsRequired();

            builder.Property(e => e.Venue)
            .HasMaxLength(500)
            .IsRequired();

            builder.Property(e => e.Longitude)
            .IsRequired();

            builder.Property(e => e.Latitude)
            .IsRequired();

        }
    }
}