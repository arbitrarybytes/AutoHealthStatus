using AutoHealthStatus.Models;

namespace AutoHealthStatus;

public static class LogHelpers
{
    const ConsoleColor InfoColor = ConsoleColor.Cyan;
    const ConsoleColor ErrorColor = ConsoleColor.Red;
    const ConsoleColor WarningColor = ConsoleColor.Yellow;
    const ConsoleColor SuccessColor = ConsoleColor.Green;

    public static void LogAsInfo(this string message) => LogMessageInternal(message, InfoColor);

    public static void LogAsWarning(this string message) => LogMessageInternal(message, WarningColor);

    public static void LogAsError(this string message) => LogMessageInternal(message, ErrorColor);

    public static void LogAsSuccess(this string message) => LogMessageInternal(message, SuccessColor);

    private static void LogMessageInternal(string message, ConsoleColor color)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = currentColor;
    }

    public static string ToMessage(this PortalConfig config, string message)
    {
        return $"[{config.Name} : {config.Authentication}] - {message}";
    }

    public static void LogError(this PortalConfig config, string errorMessage, string source)
    {
        $"[{config.Name} : {config.Authentication}] - Exception at {source}\nDetails: {errorMessage}".LogAsError();
    }
}