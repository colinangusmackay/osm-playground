namespace OsmPlayground.Data.Entities;

public abstract class OsmRelationMember
{
    public required long RelationId { get; init; }

    public required int Index { get; init; }

    public OsmRelationMemberType Type { get; protected init; }

    public OsmRelationMemberTypeLookup? TypeLookup { get; init; }

    public required long RefId { get; init; }

    public string? Role { get; init; }
}
