namespace MessageBus
{
    internal class CompositionRoot
    {
        public void Init()
        {
            var container = new Container();
            container.Register<RabbitMQAdapter, DefaultRabbitMQAdapter>();
        }

    }
}