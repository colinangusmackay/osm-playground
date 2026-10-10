using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmRelationRelationRefConfiguration : IEntityTypeConfiguration<OsmRelationRelationRef>
{
    public void Configure(EntityTypeBuilder<OsmRelationRelationRef> builder)
    {
        builder.HasOne(rr => rr.Relation)
            .WithMany()
            .HasForeignKey(rr => rr.RelationRefId)
            .IsRequired(false)
            // A relation can't be deleted while a parent relation uses it;
            // remove it from the parent first.
            .OnDelete(DeleteBehavior.Restrict);
    }
}