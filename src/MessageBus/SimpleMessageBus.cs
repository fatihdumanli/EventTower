namespace MessageBus
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
            _container.Register<RabbitMQAdapter>(delegate
            {
                return new DefaultRabbitMQAdapter(name);
            });

            var rabbitMqAdapter = _container.Create<RabbitMQAdapter>();

            return new MessageBusEndpoint(rabbitMqAdapter);
        }
    }
}