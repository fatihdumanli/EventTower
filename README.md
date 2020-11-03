
# ServiceTower
A service bus implementation created with RabbitMQ. 

# Installation
.NET Core CLI

`dotnet add package ServiceTower`

NuGet package manager

`Install-Package ServiceTower`

# Usage

## Setting up an endpoint

Create a messaging enpoint and call Start() method.

    var endpoint = Endpoint.Create("sender");
    endpoint.Start()
   
  ## Sending commands
  
  Create a command class implements `ServiceTower.ICommand` interface and use `Send()` method to send the command to the destination endpoint.

    var command =  new  CreateOrderCommand();
    endpoint.Send(command, "destinationEndpoint");

## Publishing events

Create an event class implements `ServiceTower.IEvent` interface and Use `Publish()` method to publish the event to all destinations.

    var @event =  new  CustomerEmailChanged();
    endpoint.Publish(@event);



