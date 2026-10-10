namespace OsmPlayground.Data.Entities;

public class OsmRelationNodeRef : OsmRelationMember
{
    public OsmRelationNodeRef()
    {
        Type = OsmRelationMemberType.Node;
    }

    // Holds RefId when the node is in the database, or null when it is missing
    // from the import (an incomplete relation). Exists so the database can hold
    // a foreign key to osm_nodes.
    public long? NodeRefId { get; init; }

    public OsmNode? Node { get; init; }
}
