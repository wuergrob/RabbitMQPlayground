Small Spike using RabbitMQ, specifically showcasing some scenarios for the "Topic" exchange type.

## Prerequisites
- A local RabbitMQ Broker must be running on the default port (5672)

Installing RabbitMQ: https://www.rabbitmq.com/docs/download

## Running the project
### Consumer
- Consumes messages from the "info" queue unless reconfigured
- `cd Consumer`
- `dotnet run`

### Producer
- Sends a message with routing-key=logs.info to a topic exchange
- Accepts args in the form of "LOGLEVEL MESSAGE" (Example: info HelloThere)
- Usage example: 
- `cd Producer`
- `dotnet run critical "pods are down"`
