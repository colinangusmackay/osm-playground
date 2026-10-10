namespace OsmPlayground.Data.Entities;

public class OsmWayNode
{
    public long WayId { get; init; }

    public int Index { get; init; }

    // The OSM id of the node, which is always known even when the node itself
    // is missing from the import.
    public long NodeId { get; init; }

    // Holds NodeId when the node is in the database, or null when it is missing
    // from the import (an incomplete way). Exists so the database can hold a
    // foreign key to osm_nodes.
    public long? NodeRefId { get; init; }

    public OsmWay? Way { get; init; }

    public OsmNode? Node { get; init; }
}
