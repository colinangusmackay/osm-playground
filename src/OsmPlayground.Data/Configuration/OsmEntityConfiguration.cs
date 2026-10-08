using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Converters;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public abstract class OsmEntityConfiguration
{
    public void Configure<TEntity>(EntityTypeBuilder<TEntity> builder)
        where TEntity : OsmEntity
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.Timestamp)
            .HasConversion<UtcDateTimeConverter>();

        builder.Property(e => e.Tags)
            .HasColumnType("hstore")
            .IsRequired();
    }
}
