using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace OsmPlayground.Data.Entities;

public abstract class OsmEntity
{
    protected static readonly GeometryFactory GeomFactory =
        NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

    public long Id { get; init; }

    public int? UserId { get; init; }

    public OsmUser? User { get; init; }

    public DateTime Timestamp { get; init; }

    public int Version { get; init; }

    public bool Visible { get; init; }

    public long Changeset { get; init; }

    public Dictionary<string, string> Tags { get; init; } = [];

    // Set on import when the entity references data that is not in the
    // database, e.g. a way or relation whose members fall outside the extract.
    public bool IsIncomplete { get; set; }
}
