

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(nameof(Comment));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(e => e.Body)
            .HasMaxLength(2000)
            .IsRequired();

            builder.HasOne(e => e.Activity)
            .WithMany(a => a.Comments)
            .HasForeignKey(e => e.ActivityId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

            builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        }
    }
}