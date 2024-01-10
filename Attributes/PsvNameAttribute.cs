namespace PSVSerializer.Attributes;

public class PsvNameAttribute : Attribute
{
    public string Name { get; }

    public PsvNameAttribute(string name)
    {
        Name = name;
    }
}