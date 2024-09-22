using RabbitMQ.Client;

namespace Shared;

public static class RabbitMqSetup
{
    public static string LogExchangeName => "logs";
    public static IModel SetupRabbitMqBroker(IConnection connection)
    {
        var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: LogExchangeName, type: ExchangeType.Topic);

        foreach (var logLevel in Enum.GetValues<LogLevel>())
        {
            DeclareDurableQueue(channel, logLevel.ToString());
            channel.QueueBind(logLevel.ToString(), LogExchangeName, logLevel.ToRoutingKey());
        }

        // Queue "GottaFixThose" wants Error and Critical logs (Multiple bindings for this queue)
        DeclareDurableQueue(channel, "GottaFixThose");
        channel.QueueBind("GottaFixThose", LogExchangeName, LogLevel.Error.ToRoutingKey());
        channel.QueueBind("GottaFixThose", LogExchangeName, LogLevel.Critical.ToRoutingKey());
        // Queue "AllLogs" wants all logs, regardless of severity (One binding for this queue by using wildcard routing key
        DeclareDurableQueue(channel, "AllLogs");
        channel.QueueBind("AllLogs", LogExchangeName, $"Logs.*");
        return channel;
    }
    
    private static void DeclareDurableQueue(IModel onChannel, string name)
    {
        var channelNameOnOk = onChannel.QueueDeclare(
            queue: name,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }
}