namespace PSVSerializer.Attributes;

public class PsvIgnoreAttribute : Attribute
{
    public bool IsIgnore { get; } = true;
}