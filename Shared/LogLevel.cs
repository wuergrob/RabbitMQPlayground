namespace Shared;

public enum LogLevel
{
    Info,
    Warning,
    Error,
    Critical
}

public static class LogLevelExtensions
{
    private const string ParentTopic = "Logs";
    public static string ToRoutingKey(this LogLevel logLevel)
    {
        return $"{ParentTopic}.{logLevel.ToString()}";
    }
}