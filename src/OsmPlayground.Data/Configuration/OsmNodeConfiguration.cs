using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmNodeConfiguration : OsmEntityConfiguration, IEntityTypeConfiguration<OsmNode>
{
    public void Configure(EntityTypeBuilder<OsmNode> builder)
    {
        builder.ToTable("osm_nodes");

        base.Configure(builder);

        builder.Ignore(n => n.Latitude);
        builder.Ignore(n => n.Longitude);

        builder.Ignore(n => n.Ways);

        builder.Property(n => n.Location).
            HasColumnType("geometry(Point, 4326)");
        builder.HasIndex(n => n.Location)
            .HasMethod("GIST");
    }
}
