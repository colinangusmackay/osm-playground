namespace OsmPlayground.Data.Entities;

public class OsmRelationNodeRef : OsmRelationMember
{
    public OsmRelationNodeRef()
    {
        Type = OsmRelationMemberType.Node;
    }

    // Mirrors RefId so the database can hold a foreign key to osm_nodes.
    // The empty setter is only there so EF can map the property.
    public long NodeId
    {
        get => RefId;
        private init { }
    }

    public OsmNode? Node { get; init; }
}
