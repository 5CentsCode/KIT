using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ToolKIT.Extensions;
public static class GenericExtensions
{
    [return: NotNull]
    public static T ThrowIfNull<T>([NotNull] this T? obj, [CallerArgumentExpression(nameof(obj))] string? name = null)
    {
        ArgumentNullException.ThrowIfNull(obj, name);
        return obj!;
    }

    public static T? ThrowIfNotType<T>(this object obj, [CallerArgumentExpression(nameof(obj))] string? name = null)
    {
        try
        {
            obj?.GetType().ThrowIfNotType<T>();
        }
        catch (Exception e)
        {
            throw new Exception($"{name} wrong type", e);

        }
        return (T?)obj;
    }
}
