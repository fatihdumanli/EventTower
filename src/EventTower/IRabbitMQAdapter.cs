namespace EventTower
{
    public delegate void MessageReceived(MessageReceivedEventArgs args);

    public interface IRabbitMQAdapter
    {
        event MessageReceived MessageReceived;
        void TryConnect();
        void BasicPublish(ICommand command, string destination);
        void BasicPublish(IEvent @event);
        void StartConsuming();
    }
}