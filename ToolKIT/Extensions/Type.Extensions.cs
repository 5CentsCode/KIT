using ToolKIT.Exceptions;

namespace ToolKIT.Extensions;

public static class TypeExtensions
{
    public static void ThrowIfNotType<T>(this Type type)
    {
        Type tType = typeof(T);
        if (tType != type)
        {
            throw new InvalidTypeException(tType, type);
        }
    }
}
