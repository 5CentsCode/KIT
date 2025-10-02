namespace ToolKIT.Exceptions;

public class InvalidTypeException : Exception
{
    private const string ExceptionMessage = "Invalid Type. [Expected: {0}] [Actual: {1}]";

    public InvalidTypeException(Type expected, Type actual)
        : base(string.Format(ExceptionMessage, expected, actual))
    {
    }
}
