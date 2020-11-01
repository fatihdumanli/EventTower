using System;
using System.Linq;
using System.Reflection;
using MessageBus.Exceptions;
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
            var assemblies = System.Reflection.Assembly.GetEntryAssembly().GetReferencedAssemblies()
                    .Select(a => Assembly.Load(a))
                    .Append(Assembly.GetEntryAssembly());

            var types = assemblies.SelectMany(w => w.GetTypes());
            var messageType = types.Where(m => m.Name.Equals(args.Type)).FirstOrDefault();

            //That means there is no using statements (using MessageBus) in the entry assembly
            //Thus, no handler. 
            //We're throwing MessageHandlerNotFoundException.
            if(messageType == null)
            {
                throw new MessageHandlerNotFoundException(args.Type);
            }

            var genericHandlerInterfaceType = typeof(IMessageHandler<>).MakeGenericType(messageType);
            var handlerClass = Assembly.GetEntryAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(genericHandlerInterfaceType));

            if(handlerClass.Count() == 0)
            {
                throw new MessageHandlerNotFoundException(args.Type);
            }
            
            if(handlerClass.Count() > 1)
            {
                throw new MultipleMessageHandlerFoundException(args.Type);
            }


            

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