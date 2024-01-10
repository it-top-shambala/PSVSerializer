using System.Reflection;
using PSVSerializer.Core;

namespace PSVSerializer.Test;

public class PsvSerializerTest
{
    [Fact]
    public void Serialize_PsvNameAttribute_ExceptionTest()
    {
        var obj = new TestObjectForException();
        Assert.Throws<CustomAttributeFormatException>(() =>
        {
            PsvSerializer.Serialize(obj);
        });
    }
}