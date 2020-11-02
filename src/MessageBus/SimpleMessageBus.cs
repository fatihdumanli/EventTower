using System;

namespace SimpleMessageBus
{
    public class SimpleMessageBus
    {
        private static Container _container;

        internal static Container Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new Container();
                }

                return _container;
            }
        }

        public static MessageBusEndpoint CreateEndpoint(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Endpoint must have a name.");
            }

            Container.Register<RabbitMQAdapter>(delegate
            {
                return new DefaultRabbitMQAdapter(name);
            });

            var rabbitMqAdapter = Container.Create<RabbitMQAdapter>();

            return new MessageBusEndpoint(rabbitMqAdapter);
        }
    }
}