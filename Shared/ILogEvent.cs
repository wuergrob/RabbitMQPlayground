namespace Shared;

public interface ILogEvent
{
    string Message { get; }
    LogLevel LogLevel { get; }
}