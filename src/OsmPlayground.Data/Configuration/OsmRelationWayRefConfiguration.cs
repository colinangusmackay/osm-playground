using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmRelationWayRefConfiguration : IEntityTypeConfiguration<OsmRelationWayRef>
{
    public void Configure(EntityTypeBuilder<OsmRelationWayRef> builder)
    {
        builder.HasOne(rw => rw.Way)
            .WithMany()
            .HasForeignKey(rw => rw.WayId);
    }
}
