using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmWayNodeConfiguration : IEntityTypeConfiguration<OsmWayNode>
{
    public void Configure(EntityTypeBuilder<OsmWayNode> builder)
    {
        builder.ToTable("osm_way_nodes");
        builder.HasKey(wn => new { wn.WayId, wn.Index });

        builder.HasOne(wn => wn.Node)
            .WithMany(n => n.WayNodes)
            .HasForeignKey(wn => wn.NodeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(wn => wn.Way)
            .WithMany(w => w.WayNodes)
            .HasForeignKey(wn => wn.WayId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
