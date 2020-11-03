using System;

namespace EventTower
{
    public class Endpoint
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

        public static MessageBusEndpoint Create(string name, string hostName = "localhost", string username = "guest", string password = "guest" )
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Endpoint must have a name.");
            }

            Container.Register<IRabbitMQAdapter>(delegate
            {
                return new DefaultRabbitMQAdapter(name, hostName, username, password);
            });

            var rabbitMqAdapter = Container.Create<IRabbitMQAdapter>();

            return new MessageBusEndpoint(rabbitMqAdapter);
        }
    }
}