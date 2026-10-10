using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace OsmPlayground.Data.Entities;

public class OsmNode : OsmEntity
{
    private Point _location = GeomFactory.CreatePoint(new Coordinate(0, 0));

    public double Latitude => _location.Y;

    public double Longitude => _location.X;

    public Point Location
    {
        get => _location;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value.SRID != GeomFactory.SRID)
                throw new ArgumentException($"Location SRID ({value.SRID}) must match the factory SRID ({GeomFactory.SRID}).", nameof(value));
            _location = value;
        }
    }

    public List<OsmWayNode> WayNodes { get; init; } = [];

    public IEnumerable<OsmWay> Ways => WayNodes
        .Where(wn => wn.Way is not null)
        .Select(wn => wn.Way!);

    public Point SetLocation(double latitude, double longitude) => Location = GeomFactory.CreatePoint(new Coordinate(longitude, latitude));
}
