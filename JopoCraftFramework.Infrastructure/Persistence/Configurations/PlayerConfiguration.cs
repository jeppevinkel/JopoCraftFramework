using JopoCraftFramework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JopoCraftFramework.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.UserId);
        builder.Property(p => p.UserId).HasMaxLength(64);
        builder.Property(p => p.Nickname).HasMaxLength(64).IsRequired();
        builder.Property(p => p.IpAddress).HasMaxLength(45);

        builder.HasMany(p => p.GameEvents)
            .WithOne(e => e.Player)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false);
    }
}
