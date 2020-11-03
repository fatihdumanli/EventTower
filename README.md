
# ServiceTower
A service bus implementation created with RabbitMQ. 

# Give a star
If you like this repository or you've learned something please give a star ⭐Thanks!


# How ServiceTower works

![ServiceTower](https://i.ibb.co/23z1z1t/rabbitmq-diagram-1.png)

ServiceTower uses a RabbitMQ.Client to establish a connection to given RabbitMQ server. Uses a abstraction layer called 'RabbitMQAdapter' to publishing/subscribing logic. For more information take a look my [blog post](https://fatihdumanli.medium.com/build-a-message-bus-implementation-with-net-core-and-rabbitmq-9ba350b777f4).


# Installation
.NET Core CLI

`dotnet add package ServiceTower`

NuGet package manager

`Install-Package ServiceTower`

# Usage

## Setting up an endpoint

Create a messaging enpoint and call Start() method.

```csharp
 var endpoint = Endpoint.Create("sender");
 endpoint.Start()
```
   
  ## Sending commands
  
  Create a command class implements `ServiceTower.ICommand` interface and use `Send()` method to send the command to the destination endpoint.

```csharp
var command =  new  CreateOrderCommand();
endpoint.Send(command, "destinationEndpoint");
```    

## Publishing events

Create an event class implements `ServiceTower.IEvent` interface and Use `Publish()` method to publish the event to all destinations.

```csharp
var @event =  new  CustomerEmailChanged();
endpoint.Publish(@event);
```
    

## Handling Commands/Events

Create a handler class that implements `IMessageHandler<T>` interface. The generic parameter must be `ICommand` or `IEvent`. A sample event handler shown below. Commands are handles in the same way.

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
