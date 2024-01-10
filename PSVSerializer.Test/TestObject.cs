using PSVSerializer.Attributes;

namespace PSVSerializer.Test;

public class TestObjectForException
{
    [PsvName("")]
    public int Id { get; set; }
}