namespace Shared.LogCategories;

public class CriticalLog(string message) : ILogEvent
{
    public string Message => message;
    public LogLevel LogLevel => LogLevel.Critical;
}