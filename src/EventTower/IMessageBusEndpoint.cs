using System;
using System.Linq;
using System.Reflection;
using EventTower.Exceptions;
using Newtonsoft.Json;
using EventTower.Utils;

namespace EventTower
{
    public interface IMessageBusEndpoint 
    {
        void Start();
        void HandleMessage(MessageReceivedEventArgs args);
        void Send(ICommand command, string destination);
        void Publish(IEvent @event);
    }
}