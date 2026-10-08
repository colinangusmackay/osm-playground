using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmRelationNodeRefConfiguration : IEntityTypeConfiguration<OsmRelationNodeRef>
{
    public void Configure(EntityTypeBuilder<OsmRelationNodeRef> builder)
    {
        builder.HasOne(rn => rn.Node)
            .WithMany()
            .HasForeignKey(rn => rn.NodeId);
    }
}
