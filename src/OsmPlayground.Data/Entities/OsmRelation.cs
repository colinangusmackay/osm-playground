namespace OsmPlayground.Data.Entities;

public class OsmRelation : OsmEntity
{
    public List<OsmRelationMember> Members { get; init; } = [];

    public IEnumerable<OsmRelationNodeRef> Nodes => Members.OfType<OsmRelationNodeRef>();

    public IEnumerable<OsmRelationWayRef> Ways => Members.OfType<OsmRelationWayRef>();
}
