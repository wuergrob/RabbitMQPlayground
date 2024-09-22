namespace Shared.LogCategories;

public class ErrorLog(string message) : ILogEvent
{
    public string Message => message;
    public LogLevel LogLevel => LogLevel.Error;
}