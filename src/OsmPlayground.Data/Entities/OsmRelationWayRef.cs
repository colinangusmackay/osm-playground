namespace OsmPlayground.Data.Entities;

public class OsmRelationWayRef : OsmRelationMember
{
    public OsmRelationWayRef()
    {
        Type = OsmRelationMemberType.Way;
    }

    // Mirrors RefId so the database can hold a foreign key to osm_ways.
    // The empty setter is only there so EF can map the property.
    public long WayId
    {
        get => RefId;
        private init { }
    }

    public OsmWay? Way { get; init; }
}
