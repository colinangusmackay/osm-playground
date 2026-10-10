using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OsmPlayground.Data.Converters;

/// <summary>
/// Stores <see cref="DateTime"/> values as UTC and marks values read back from
/// the database as <see cref="DateTimeKind.Utc"/>.
/// </summary>
public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter()
        : base(
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
