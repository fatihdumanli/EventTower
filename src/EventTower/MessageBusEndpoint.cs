using System;
using System.Linq;
using System.Reflection;
using EventTower.Exceptions;
using Newtonsoft.Json;
using EventTower.Utils;

namespace EventTower
{
    public class MessageBusEndpoint 
    {
        private readonly IRabbitMQAdapter rabbitMqAdapter;
        private readonly IReflectionUtil reflectionUtil = new ReflectionUtil();

        public MessageBusEndpoint(IRabbitMQAdapter rabbitMQadapter)
        {
            rabbitMqAdapter = rabbitMQadapter;
        }

        public void Start()
        {
            rabbitMqAdapter.MessageReceived += RabbitMqAdapter_MessageReceived;
            rabbitMqAdapter.StartConsuming();
        }

        private void RabbitMqAdapter_MessageReceived(MessageReceivedEventArgs args)
        {
            var assemblies = reflectionUtil.GetAssemblies();
            var types = reflectionUtil.GetTypes(assemblies);

            var messageType = types.Where(m => m.Name.Equals(args.Type)).FirstOrDefault();

            //That means there is no using statements (using MessageBus) in the entry assembly
            //Thus, no handler. 
            //We're throwing MessageHandlerNotFoundException.
            if(messageType == null)
            {
                throw new MessageHandlerNotFoundException(args.Type);
            }

            var genericHandlerInterfaceType = typeof(IMessageHandler<>).MakeGenericType(messageType);
            var handlerClassLookup = Assembly.GetEntryAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(genericHandlerInterfaceType));


            if (handlerClassLookup.Count() == 0)
            {
                throw new MessageHandlerNotFoundException(args.Type);
            }
            
            if(handlerClassLookup.Count() > 1)
            {
                throw new MultipleMessageHandlerFoundException(args.Type);
            }

            var handlerClass = handlerClassLookup.First();
            var handlerInstance = Activator.CreateInstance(handlerClass);

            var message = JsonConvert.DeserializeObject(args.Payload.ToString(), messageType);
            handlerClass.GetMethod("Handle").Invoke(handlerInstance, new[] { message });
        }


        /// <summary>
        /// Sends the command to the given endpoint.
        /// </summary>
        /// <param name="command">Command to send.</param>
        /// <param name="endpoint">The endpoint which is the command will be sent to.</param>
        public void Send(ICommand command, string endpoint)
        {
            if(string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentException("An endpoint must be provided to send a command.");
            }

            rabbitMqAdapter.BasicPublish(command, endpoint);                    
        }
        
      
        /// <summary>
        /// Publishes the event to all endpoints.
        /// </summary>
        public void Publish(IEvent @event)
        {
            rabbitMqAdapter.BasicPublish(@event);
        }
      
    }
}