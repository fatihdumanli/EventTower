
# ServiceTower
A service bus implementation created with RabbitMQ. 

# Installation
.NET Core CLI

`dotnet add package ServiceTower`

NuGet package manager

`Install-Package ServiceTower`

# Usage
Create a messaging enpoint and call Start() method.

`var endpoint = Endpoint.Create("emitter");`
`endpoint.Start();`
