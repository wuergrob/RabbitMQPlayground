namespace Shared.LogCategories;

public class InfoLog(string message) : ILogEvent
{
    public string Message => message;
    public LogLevel LogLevel => LogLevel.Info;
}