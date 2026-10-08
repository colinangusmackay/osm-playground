namespace OsmPlayground.Data.Entities;

public class OsmUser
{
    public int Id { get; init; }

    public string? DisplayName { get; set; }

    public DateTime FirstSeen { get; set; }

    public DateTime LastSeen { get; set; }
}
