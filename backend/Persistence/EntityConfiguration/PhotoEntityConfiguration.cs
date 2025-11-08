

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    public class PhotoEntityConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable(nameof(Photo));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME2")
            .IsRequired();

            builder.Property(e => e.UpdatedAt)
            .HasColumnType("DATETIME2")
            .IsRequired();

            builder.HasOne(e => e.User)
            .WithMany(u => u.Photos)
            .HasForeignKey(e => e.UserId);

            builder.Property(e => e.PublicId)
            .HasMaxLength(500)
            .IsRequired();

            builder.Property(e => e.Url)
            .HasMaxLength(500)
            .IsRequired();

        }
    }
}