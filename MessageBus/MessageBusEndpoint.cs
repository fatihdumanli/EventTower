using System;
using MessageBus.Extensions;
using RabbitMQ.Client;

namespace MessageBus
{
    public class MessageBusEndpoint 
    {
        /// <summary>
        /// Sending/Publishing can be performed via multiple channels, but subscribing/consuming channel must be single.
        /// </summary>
        private readonly string endpointName;
        private readonly RabbitMQAdapter rabbitMqAdapter;

        public MessageBusEndpoint(string name)
        {
            endpointName = name;
            rabbitMqAdapter = new RabbitMQAdapter(name);
            rabbitMqAdapter.StartConsuming();
            rabbitMqAdapter.MessageReceived += RabbitMqAdapter_MessageReceived;
        }

        private void RabbitMqAdapter_MessageReceived(MessageReceivedEventArgs args)
        {
            Console.WriteLine("a event is received");
        }


        /// <summary>
        /// Sends the command to the given endpoint.
        /// </summary>
        /// <param name="command">Command to send.</param>
        /// <param name="endpoint">The endpoint which is the command will be sent to.</param>
        public void Send(ICommand command, string endpoint)
        {
            rabbitMqAdapter.BasicPublish(command, endpoint);                    
        }
        
      
    }
}