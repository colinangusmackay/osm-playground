using System.Globalization;

namespace OsmCli;

public sealed record BoundingBox(double North, double West, double South, double East)
{
    /// <summary>
    /// Parses a bounding box in the form <c>north_lat,west_long,south_lat,east_long</c>.
    /// </summary>
    public static bool TryParse(string value, out BoundingBox? boundingBox, out string? error)
    {
        boundingBox = null;
        error = null;

        var parts = value.Split(',', StringSplitOptions.TrimEntries);
        if (parts.Length != 4)
        {
            error = $"Bounding box '{value}' must have four comma separated values: north_lat,west_long,south_lat,east_long.";
            return false;
        }

        var coordinates = new double[4];
        for (var i = 0; i < parts.Length; i++)
        {
            if (!double.TryParse(parts[i], NumberStyles.Float, CultureInfo.InvariantCulture, out coordinates[i]))
            {
                error = $"Bounding box value '{parts[i]}' is not a valid number.";
                return false;
            }
        }

        var (north, west, south, east) = (coordinates[0], coordinates[1], coordinates[2], coordinates[3]);

        if (north is < -90 or > 90 || south is < -90 or > 90)
        {
            error = "Bounding box latitudes must be between -90 and 90.";
            return false;
        }

        if (west is < -180 or > 180 || east is < -180 or > 180)
        {
            error = "Bounding box longitudes must be between -180 and 180.";
            return false;
        }

        if (north < south)
        {
            error = "Bounding box north latitude must not be less than the south latitude.";
            return false;
        }

        boundingBox = new BoundingBox(north, west, south, east);
        return true;
    }
}
