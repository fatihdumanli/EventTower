
# EventTower
A message bus implementation with functionality sending commands/events to other processes by using RabbitMQ.

# Give a star
If you like this repository or you've learned something please give a star ⭐Thanks!

# How EventTower works

![EventTower](https://i.ibb.co/23z1z1t/rabbitmq-diagram-1.png)

EventTower uses RabbitMQ.Client package to establish a connection to the given RabbitMQ server. Uses an abstraction layer called 'RabbitMQAdapter' to publishing/subscribing logic. For more information take a look at my [blog post](https://fatihdumanli.medium.com/build-a-message-bus-implementation-with-net-core-and-rabbitmq-9ba350b777f4).


# Installation
.NET Core CLI

`dotnet add package EventTower`

NuGet package manager

`Install-Package EventTower`

# Usage

## Setting up an endpoint

Create a messaging endpoint and call Start() method.

```csharp
 var endpoint = Endpoint.Create("sender");
 endpoint.Start()
```
   
  ## Sending commands
  
  Create a command class implements the `EventTower.ICommand` interface and use `Send()` method to send the command to the destination endpoint.

```csharp
var command =  new  CreateOrderCommand();
endpoint.Send(command, "destinationEndpoint");
```    

## Publishing events

Create an event class implements the `EventTower.IEvent` interface and Use `Publish()` method to publish the event to all destinations.

```csharp
var @event =  new  CustomerEmailChanged();
endpoint.Publish(@event);
```
    

## Handling Commands/Events

Create a handler class that implements `IMessageHandler<T>` interface. The generic parameter must be `ICommand` or `IEvent`. A sample event handler is shown below. Commands are handles in the same way.

```csharp
public  class  EventHandler :
	IMessageHandler<CustomerEmailChanged>
{
	public  Task  Handle(CustomerEmailChanged message)
	{
		Console.WriteLine("An CustomerEmailChanged event received. Content: {0}", JsonConvert.SerializeObject(message));
		return Task.CompletedTask;
	}
}
```

## Runtime Demo
![Runtime demo](https://i.ibb.co/Qmdj55L/ezgif-7-d5b57d8cc3de.gif)
