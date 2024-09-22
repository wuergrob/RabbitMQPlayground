using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;

var factory = new ConnectionFactory { HostName = "localhost"};
using var connection = factory.CreateConnection();
using var channel = RabbitMqSetup.SetupRabbitMqBroker(connection);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var routingKey = ea.RoutingKey;
    Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
};

channel.BasicConsume(queue: LogLevel.Info.ToString(),
    autoAck: true,
    consumer: consumer);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
