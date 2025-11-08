

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    public class AttendeesEntityConfiguration : IEntityTypeConfiguration<Attendees>
    {
        public void Configure(EntityTypeBuilder<Attendees> builder)
        {
            builder.ToTable(nameof(Attendees));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME2")
            .IsRequired();

            builder.Property(e => e.UpdatedAt)
            .HasColumnType("DATETIME2")
            .IsRequired();

            builder.Property(e => e.IsHost)
            .IsRequired();

            builder.Property(e => e.IsAttending)
            .IsRequired();

            builder.HasOne(e => e.Activity)
            .WithMany(a => a.Attendees)
            .HasForeignKey(e => e.ActivityId);

            builder.HasOne(e => e.User)
            .WithMany(u => u.Attendees)
            .HasForeignKey(e => e.UserId);
        }
    }
}