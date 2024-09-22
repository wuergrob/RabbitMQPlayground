namespace Shared.LogCategories;

public class WarningLog(string message) : ILogEvent
{
    public string Message => message;
    public LogLevel LogLevel => LogLevel.Warning;
}