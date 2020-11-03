using System;
using System.Linq;
using System.Reflection;
using ServiceTower.Exceptions;
using Newtonsoft.Json;
using ServiceTower.Utils;

namespace ServiceTower
{
    public interface IMessageBusEndpoint 
    {
        void Start();
        void HandleMessage(MessageReceivedEventArgs args);
        void Send(ICommand command, string destination);
        void Publish(IEvent @event);
    }
}