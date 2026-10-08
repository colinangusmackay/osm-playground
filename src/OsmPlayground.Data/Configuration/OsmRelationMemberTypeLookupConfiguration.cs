using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmRelationMemberTypeLookupConfiguration : IEntityTypeConfiguration<OsmRelationMemberTypeLookup>
{
    public void Configure(EntityTypeBuilder<OsmRelationMemberTypeLookup> builder)
    {
        builder.ToTable("osm_relation_member_types");

        builder.HasKey(rmtl => rmtl.Id);

        builder.HasData(
            new OsmRelationMemberTypeLookup{Id = OsmRelationMemberType.Node, Name = nameof(OsmRelationMemberType.Node)},
            new OsmRelationMemberTypeLookup{Id = OsmRelationMemberType.Way, Name = nameof(OsmRelationMemberType.Way)},
            new OsmRelationMemberTypeLookup{Id = OsmRelationMemberType.Relation, Name = nameof(OsmRelationMemberType.Relation)});
    }
}
