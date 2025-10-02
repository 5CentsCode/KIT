using ToolKIT.Extensions;

namespace ToolKitTests.Extensions;


public class GenericExtensionTests
{

    private struct TestStruct
    {
    }

    private class TestClass
    {
    }

    [Theory]
    [InlineData(typeof(int))]
    [InlineData(typeof(TestStruct))]
    [InlineData(typeof(TestClass))]
    [InlineData(typeof(List<int>))]
    [InlineData(typeof(Dictionary<int, string>))]
    [InlineData(typeof(string))]
    public void ThrowIfNull_NullValue_ThrowsException(Type type)
    {
        // Arrange
        object? obj = CreateInstance(type);
        obj = null;

        // Act + Assert
        Assert.Throws<ArgumentNullException>(() => obj.ThrowIfNull());
    }

    [Theory]
    [InlineData(typeof(int))]
    [InlineData(typeof(TestStruct))]
    [InlineData(typeof(TestClass))]
    [InlineData(typeof(List<int>))]
    [InlineData(typeof(Dictionary<int, string>))]
    [InlineData(typeof(string))]
    public void ThrowIfNull_NonNullValue_ReturnsValue(Type type)
    {
        // Arrange
        object? obj = CreateInstance(type);

        // Act
        object obj2 = obj.ThrowIfNull();

        // Assert
        Assert.NotNull(obj2);
    }

    private static object? CreateInstance(Type type)
    {
        if (type == typeof(string))
        {
            return string.Empty;
        }

        object? obj = Activator.CreateInstance(type);
        return obj;
    }
}
