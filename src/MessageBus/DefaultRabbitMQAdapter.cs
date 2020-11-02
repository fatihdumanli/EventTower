using System;
using System.Net.Sockets;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace SimpleMessageBus
{   
    public class DefaultRabbitMQAdapter : RabbitMQAdapter
    {        
        public DefaultRabbitMQAdapter(string name) : base(name)
        {
            
        }
    
    }
}