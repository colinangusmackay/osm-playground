using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace OsmPlayground.Data.Entities;

public class OsmNode : OsmEntity
{
    private Point _location = GeomFactory.CreatePoint(new Coordinate(0, 0));

    public double Latitude
    {
        get => _location.Y;
        set => _location = CreatePoint(Longitude, value);
    }

    public double Longitude
    {
        get => _location.X;
        set => _location = CreatePoint(value, Latitude);
    }

    public Point Location
    {
        get => _location;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _location = value;
        }
    }

    public List<OsmWayNode> WayNodes { get; init; } = [];

    public IEnumerable<OsmWay> Ways => WayNodes.Select(wn => wn.Way);

    private static Point CreatePoint(double longitude, double latitude) => GeomFactory.CreatePoint(new Coordinate(longitude, latitude));
}
