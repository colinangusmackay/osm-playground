namespace OsmPlayground.Data.Entities;

public class OsmWayNode
{
    public long WayId { get; init; }

    public int Index { get; init; }

    public long NodeId { get; init; }

    public OsmWay Way { get; init; }

    public OsmNode Node { get; init; }
}
