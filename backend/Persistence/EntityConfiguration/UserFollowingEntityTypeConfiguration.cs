

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    public class UserFollowingEntityTypeConfiguration : IEntityTypeConfiguration<UserFollowing>
    {
        public void Configure(EntityTypeBuilder<UserFollowing> builder)
        {
            builder.ToTable(nameof(UserFollowing));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(e => e.UpdatedAt)
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.HasOne(e => e.Target)
            .WithMany(u => u.Followings)
            .HasForeignKey(e => e.TargetId)
            .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(e => e.Observer)
            .WithMany(u => u.Followers)
            .HasForeignKey(e => e.ObserverId)
            .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}