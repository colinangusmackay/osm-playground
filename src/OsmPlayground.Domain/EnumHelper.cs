namespace OsmPlayground.Domain;

public class EnumHelper
{
    public static int MaxLength<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetNames<TEnum>()
            .Max(x => x.Length);
    }
}
