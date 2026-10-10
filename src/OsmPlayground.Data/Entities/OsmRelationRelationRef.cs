namespace OsmPlayground.Data.Entities;

public class OsmRelationRelationRef : OsmRelationMember
{
    public OsmRelationRelationRef()
    {
        Type = OsmRelationMemberType.Relation;
    }

    // Holds RefId when the relation is in the database, or null when it is missing
    // from the import (an incomplete relation). Exists so the database can hold
    // a foreign key to osm_relations.
    public long? RelationRefId { get; init; }

    public OsmRelation? Relation { get; init; }
}