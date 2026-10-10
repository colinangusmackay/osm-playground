namespace OsmPlayground.Data.Entities;

public class OsmRelationWayRef : OsmRelationMember
{
    public OsmRelationWayRef()
    {
        Type = OsmRelationMemberType.Way;
    }

    // Holds RefId when the way is in the database, or null when it is missing
    // from the import (an incomplete relation). Exists so the database can hold
    // a foreign key to osm_ways.
    public long? WayRefId { get; init; }

    public OsmWay? Way { get; init; }
}
