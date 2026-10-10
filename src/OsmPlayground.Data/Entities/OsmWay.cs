using NetTopologySuite.Geometries;

namespace OsmPlayground.Data.Entities;

public class OsmWay : OsmEntity
{
    public List<OsmWayNode> WayNodes { get; init; } = [];

    public Geometry? Geometry { get; set; }

    public IEnumerable<OsmNode> Nodes => WayNodes
        .OrderBy(wn => wn.Index)
        .Where(wn => wn.Node is not null)
        .Select(x => x.Node!);

    public void CreateGeometryFromAssociatedNodes()
    {
        if (WayNodes.Count == 0)
        {
            return;
        }

        if (WayNodes.Any(wn => wn.Node is null && wn.NodeRefId is not null))
        {
            throw new InvalidOperationException(
                $"Way {Id} references nodes that exist but are not loaded. Include WayNodes.Node before creating the geometry.");
        }

        // A node with neither a navigation nor a foreign key is missing from the
        // import. A partial geometry would misrepresent the way, so have none.
        IsIncomplete = WayNodes.Any(wn => wn.Node is null);
        if (IsIncomplete)
        {
            Geometry = null;
            return;
        }

        var coordinates = Nodes.Select(n => n.Location.Coordinate).ToArray();
        if (coordinates.Length > 1)
        {
            // A valid polygon needs at least four coordinates (three distinct
            // points plus the closing one); anything shorter stays a line.
            if (coordinates.Length >= 4 && coordinates[0].Equals(coordinates[^1]))
            {
                Geometry = GeomFactory.CreatePolygon(coordinates);
            }
            else
            {
                Geometry = GeomFactory.CreateLineString(coordinates);
            }
        }
        else
        {
            Geometry = GeomFactory.CreatePoint(coordinates[0]);
        }
    }
}
