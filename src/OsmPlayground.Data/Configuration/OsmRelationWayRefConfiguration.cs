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
            .HasForeignKey(rw => rw.WayRefId)
            .IsRequired(false)
            // A way can't be deleted while a relation uses it; remove it from
            // the relation first.
            .OnDelete(DeleteBehavior.Restrict);
    }
}
