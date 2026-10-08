using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Converters;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmUserConfiguration : IEntityTypeConfiguration<OsmUser>
{
    public void Configure(EntityTypeBuilder<OsmUser> builder)
    {
        builder.ToTable("osm_users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.DisplayName)
            .HasMaxLength(255);

        builder.Property(u => u.FirstSeen)
            .HasConversion<UtcDateTimeConverter>()
            .IsRequired();

        builder.Property(u => u.LastSeen)
            .HasConversion<UtcDateTimeConverter>()
            .IsRequired();
    }
}