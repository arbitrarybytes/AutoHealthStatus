namespace AutoHealthStatus;

public static class ConsoleHelpers
{
    const ConsoleColor InfoColor = ConsoleColor.Cyan;
    const ConsoleColor ErrorColor = ConsoleColor.Red;
    const ConsoleColor WarningColor = ConsoleColor.Yellow;

    public static void LogAsInfo(this string message) => LogMessageInternal(message, InfoColor);

    public static void LogAsWarning(this string message) => LogMessageInternal(message, WarningColor);

    public static void LogAsError(this string message) => LogMessageInternal(message, ErrorColor);

    private static void LogMessageInternal(string message, ConsoleColor color)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = currentColor;
    }
}