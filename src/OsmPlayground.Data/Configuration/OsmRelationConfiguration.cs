using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmRelationConfiguration : OsmEntityConfiguration, IEntityTypeConfiguration<OsmRelation>
{
    public void Configure(EntityTypeBuilder<OsmRelation> builder)
    {
        builder.ToTable("osm_relations");

        base.Configure(builder);

        builder.HasMany(r => r.Members)
            .WithOne()
            .HasForeignKey(rm => rm.RelationId);

        builder.Ignore(r => r.Nodes);
        builder.Ignore(r => r.Ways);
    }
}
