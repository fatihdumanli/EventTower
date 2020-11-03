using System;
using System.Linq;
using System.Reflection;
using MessageBus.Exceptions;
using Newtonsoft.Json;
using SimpleMessageBus.Utils;

namespace SimpleMessageBus
{
    public interface IMessageBusEndpoint 
    {
        void Start();
        void HandleMessage(MessageReceivedEventArgs args);
        void Send(ICommand command, string destination);
        void Publish(IEvent @event);
    }
}