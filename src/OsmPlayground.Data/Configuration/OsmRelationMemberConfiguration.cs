using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmRelationMemberConfiguration : IEntityTypeConfiguration<OsmRelationMember>
{
    public void Configure(EntityTypeBuilder<OsmRelationMember> builder)
    {
        builder.ToTable("osm_relation_members");

        builder.HasKey(rm => new { rm.RelationId, rm.Index });

        builder.HasDiscriminator(rm => rm.Type)
            .HasValue<OsmRelationNodeRef>(OsmRelationMemberType.Node)
            .HasValue<OsmRelationWayRef>(OsmRelationMemberType.Way);

        builder.HasOne(rm => rm.TypeLookup)
            .WithMany()
            .HasForeignKey(rm => rm.Type)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
