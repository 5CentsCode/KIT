using System.Collections;
using ToolKIT.Extensions;

namespace ToolKitTests.Extensions;
public class IListExtensionTests
{
    private IList list = new List<int>() { 1, 2, 3 };

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(int.MaxValue, 0)]
    [InlineData(0, int.MaxValue)]
    public void Swap_IndexOutOfRange_ThrowsException(int indexA, int indexB)
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(indexA, indexB));
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 0)]
    [InlineData(2, 0)]
    public void Swap_IndexesInRange_IndicesSwapped(int indexA, int indexB)
    {
        // Arrange
        IList originalList = list.Cast<int>().ToList();

        // Act
        list.Swap(indexA, indexB);

        // Assert
        Assert.Equal(originalList[indexA], list[indexB]);
        Assert.Equal(originalList[indexB], list[indexA]);
    }
}
