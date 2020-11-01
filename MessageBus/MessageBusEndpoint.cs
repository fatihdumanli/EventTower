using RabbitMQ.Client;

namespace MessageBus
{
    public class MessageBusEndpoint 
    {
        private const string EXCHANGE_NAME = "MessageBus";


        public MessageBusEndpoint(string name)
        {
        }

        public void Send(ICommand command)
        {
            var connection = ConnectionProvider.Get();

            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(EXCHANGE_NAME, type: ExchangeType.Direct);

            }

        }
        
    }
}