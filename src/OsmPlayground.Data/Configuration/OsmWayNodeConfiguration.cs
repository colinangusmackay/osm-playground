using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmWayNodeConfiguration : IEntityTypeConfiguration<OsmWayNode>
{
    public void Configure(EntityTypeBuilder<OsmWayNode> builder)
    {
        // node_ref_id mirrors node_id so it can carry a foreign key, and is null
        // when the node is missing from the import.
        builder.ToTable("osm_way_nodes", t =>
            t.HasCheckConstraint(
                "ck_osm_way_nodes_node_ref_matches_node_id",
                "node_ref_id IS NULL OR node_ref_id = node_id"));

        builder.HasKey(wn => new { wn.WayId, wn.Index });

        builder.Property(wn => wn.NodeId)
            .IsRequired();

        builder.HasOne(wn => wn.Node)
            .WithMany(n => n.WayNodes)
            .HasForeignKey(wn => wn.NodeRefId)
            // A node can't be deleted while a way uses it.
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(wn => wn.Way)
            .WithMany(w => w.WayNodes)
            .HasForeignKey(wn => wn.WayId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
