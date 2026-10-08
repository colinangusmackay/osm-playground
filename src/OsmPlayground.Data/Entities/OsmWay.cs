using NetTopologySuite.Geometries;

namespace OsmPlayground.Data.Entities;

public class OsmWay : OsmEntity
{
    public List<OsmWayNode> WayNodes { get; init; } = [];

    public Geometry Geometry { get; set; }

    public IEnumerable<OsmNode> Nodes => WayNodes
        .OrderBy(wn => wn.Index)
        .Select(x => x.Node);

    public void CreateGeometryFromAssociatedNodes()
    {
        if (WayNodes.Count == 0)
        {
            return;
        }

        var coordinates = Nodes.Select(n => n.Location.Coordinate).ToArray();
        if (coordinates.Length > 1)
        {
            if (coordinates[0].Equals(coordinates[^1]))
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
