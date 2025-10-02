using ToolKIT.Exceptions;
using ToolKIT.Extensions;

namespace ToolKitTests.Extensions;
public class TypeExtensionTests
{
    private struct TestStruct
    {
    }

    private class ParentTestClass : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    private class ChildTestClass : ParentTestClass
    {
    }

    [Theory]
    [InlineData(typeof(int))]
    [InlineData(typeof(TestStruct))]
    [InlineData(typeof(ParentTestClass))]
    [InlineData(typeof(List<int>))]
    [InlineData(typeof(Dictionary<int, string>))]
    [InlineData(typeof(string))]
    public void ThrowIfNotType_TypeDifference_ExceptionThrown(Type type)
    {
        // Arrange + Act + Assert
        Assert.Throws<InvalidTypeException>(() => type.ThrowIfNotType<object>());
    }

    [Fact]
    public void ThrowIfNotType_SameType_ExceptionNotThrown()
    {
        // Arrange + Act + Assert
        typeof(int).ThrowIfNotType<int>();
        typeof(TestStruct).ThrowIfNotType<TestStruct>();
        typeof(ParentTestClass).ThrowIfNotType<ParentTestClass>();
        typeof(List<int>).ThrowIfNotType<List<int>>();
        typeof(Dictionary<int, string>).ThrowIfNotType<Dictionary<int, string>>();
        typeof(string).ThrowIfNotType<string>();
    }

    [Fact]
    public void ThrowIfNotType_ImplementedType_ExceptionThrown()
    {
        Assert.Throws<InvalidTypeException>(() => typeof(ParentTestClass).ThrowIfNotType<IDisposable>());
        Assert.Throws<InvalidTypeException>(() => typeof(ChildTestClass).ThrowIfNotType<ParentTestClass>());
    }
}
