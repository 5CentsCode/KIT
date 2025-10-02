using System.Collections;

namespace ToolKIT.Extensions;

public static class IListExtensions
{
    public static void Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        ((IList)list).Swap(indexA, indexB);
    }

    public static void Swap(this IList list, int indexA, int indexB)
    {
        (list[indexB], list[indexA]) = (list[indexA], list[indexB]);
    }
}
