using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsmPlayground.Data.Entities;

namespace OsmPlayground.Data.Configuration;

public class OsmRelationMemberConfiguration : IEntityTypeConfiguration<OsmRelationMember>
{
    public void Configure(EntityTypeBuilder<OsmRelationMember> builder)
    {
        builder.ToTable("osm_relation_members", t =>
            t.HasCheckConstraint(
                "ck_osm_relation_members_ref_matches_type",
                RefMatchesTypeSql()));

        builder.HasKey(rm => new { rm.RelationId, rm.Index });

        builder.Property(rm => rm.Type)
            .HasConversion<int>();

        builder.Property(rm => rm.Role)
            .HasMaxLength(255);

        builder.HasDiscriminator(rm => rm.Type)
            .HasValue<OsmRelationNodeRef>(OsmRelationMemberType.Node)
            .HasValue<OsmRelationWayRef>(OsmRelationMemberType.Way)
            .HasValue<OsmRelationRelationRef>(OsmRelationMemberType.Relation);

        builder.HasOne(rm => rm.TypeLookup)
            .WithMany()
            .HasForeignKey(rm => rm.Type)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }

    // Each member type mirrors ref_id into its own column so it can carry a
    // foreign key. That column must equal ref_id, or be null when the member is
    // missing from the import, and the other type-specific columns must be
    // null. Column names are the snake_case names produced by
    // UseSnakeCaseNamingConvention().
    private static string RefMatchesTypeSql()
    {
        (OsmRelationMemberType Type, string Column)[] mirrors =
        [
            (OsmRelationMemberType.Node, "node_ref_id"),
            (OsmRelationMemberType.Way, "way_ref_id"),
            (OsmRelationMemberType.Relation, "relation_ref_id"),
        ];

        return string.Join(" OR ", mirrors.Select(m =>
        {
            var others = mirrors
                .Where(o => o.Type != m.Type)
                .Select(o => $" AND {o.Column} IS NULL");
            return $"(type = {(int)m.Type} AND ({m.Column} IS NULL OR {m.Column} = ref_id){string.Concat(others)})";
        }));
    }
}
