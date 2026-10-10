using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmWayConfiguration : OsmEntityConfiguration<OsmWay>
{
    public override void Configure(EntityTypeBuilder<OsmWay> builder)
    {
        builder.ToTable("osm_ways");
        base.Configure(builder);

        builder.Ignore(w => w.Nodes);

        // Null when the way is incomplete or has no nodes.
        builder.Property(w => w.Geometry)
            .IsRequired(false)
            .HasColumnType("geometry(Geometry, 4326)");

        builder.HasIndex(w => w.Geometry)
            .HasMethod("GIST");
    }
}
