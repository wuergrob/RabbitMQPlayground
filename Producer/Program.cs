using System.Text;
using RabbitMQ.Client;
using Shared;

var factory = new ConnectionFactory { HostName = "localhost"};
using var connection = factory.CreateConnection();
using var channel = RabbitMqSetup.SetupRabbitMqBroker(connection);

var logLevelInput = (args.Length > 0) ? args[0] : null;
var messageInput = (args.Length > 1)
    ? string.Join(" ", args.Skip(1).ToArray())
    : "Hello World!";

var logLevel = Enum.GetValues<LogLevel>().SingleOrDefault(l => string.Equals(l.ToString(), logLevelInput, StringComparison.InvariantCultureIgnoreCase));
logLevel = logLevel == default ? LogLevel.Info : logLevel;

var body = Encoding.UTF8.GetBytes(messageInput);
// Note: If no queue is bound to the exchange, the published message will be discarded
channel.BasicPublish(
    exchange: "logs",
    routingKey: logLevel.ToRoutingKey(),
    basicProperties: null,
    body: body,
    mandatory: false);

Console.WriteLine($" [x] Sent '{logLevel.ToRoutingKey()}':'{messageInput}'");