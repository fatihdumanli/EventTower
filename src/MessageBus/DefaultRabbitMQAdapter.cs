using System;
using System.Net.Sockets;
using MessageBus.Extensions;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace MessageBus
{   
    public class DefaultRabbitMQAdapter : RabbitMQAdapter
    {        
        public DefaultRabbitMQAdapter(string name) : base(name)
        {
            
        }
    
    }
}