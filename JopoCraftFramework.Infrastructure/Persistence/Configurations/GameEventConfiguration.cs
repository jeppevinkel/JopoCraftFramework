using JopoCraftFramework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JopoCraftFramework.Infrastructure.Persistence.Configurations;

public class GameEventConfiguration : IEntityTypeConfiguration<GameEvent>
{
    public void Configure(EntityTypeBuilder<GameEvent> builder)
    {
        builder.HasKey(e => e.EventId);
        builder.Property(e => e.EventType).HasMaxLength(64).IsRequired();
        builder.Property(e => e.ServerId).HasMaxLength(64);
        builder.Property(e => e.UserId).HasMaxLength(64);
        builder.Property(e => e.Payload).HasColumnType("jsonb").IsRequired();
    }
}
