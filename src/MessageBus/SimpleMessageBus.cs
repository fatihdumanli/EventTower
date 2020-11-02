namespace MessageBus
{
    public class SimpleMessageBus
    {
        public static MessageBusEndpoint Create(string name)
        {
            var container = new Container();
            return new MessageBusEndpoint(new DefaultRabbitMQAdapter(name));
        }
    }
}