using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Converters;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public abstract class OsmEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : OsmEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Version)
            .IsRequired();

        builder.Property(e => e.Visible)
            .IsRequired();

        builder.Property(e => e.Changeset)
            .IsRequired();

        builder.Property(e => e.IsIncomplete)
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.Timestamp)
            .IsRequired()
            .HasConversion<UtcDateTimeConverter>();

        builder.Property(e => e.Tags)
            .HasColumnType("hstore")
            .IsRequired();

        builder.HasIndex(e => e.Tags)
            .HasMethod("gin")
            .HasOperators("gin_hstore_ops");
    }
}
