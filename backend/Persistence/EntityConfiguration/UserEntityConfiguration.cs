

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(e => e.UpdatedAt)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(e => e.PhoneNumber)
            .IsRequired(false)
            .HasMaxLength(50);

            builder.Property(e => e.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(1000);

            builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(e => e.UserName)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);
        }
    }
}