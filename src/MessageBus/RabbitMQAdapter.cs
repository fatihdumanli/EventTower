using System;
using System.Net.Sockets;
using MessageBus.Extensions;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace MessageBus
{
    public delegate void Notify(MessageReceivedEventArgs args);

    public abstract class RabbitMQAdapter
    {
        private IModel consumerChannel;
        private bool _isConnected = false;
        private const string COMMAND_EXCHANGE_NAME = "CommandExchange";
        private const string EVENT_EXCHANGE_NAME = "EventExchange";
        private int retryCount = 3;

        public event Notify MessageReceived;
        private string queueName;

     
        public RabbitMQAdapter(string endpointName)
        {
            queueName = endpointName;
        }
        

        private IConnection _connection;
        ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

        private void TryConnect()
        {
            var policy = RetryPolicy.Handle<SocketException>().Or<BrokerUnreachableException>()
                .WaitAndRetry(retryCount, op => TimeSpan.FromSeconds(Math.Pow(2, op)), (ex, time) =>
                {
                    Console.WriteLine("Couldn't connect to RabbitMQ server...");
                });

            policy.Execute(() =>
            {
                _connection = factory.CreateConnection();
                _isConnected = true;
                Console.WriteLine("connected!");
            });

        }

        private IConnection connection
        {
            get
            {
                if (!_isConnected)
                {
                    TryConnect();
                }

                return _connection;
            }
        }

        public virtual void BasicPublish(ICommand command, string destination)
        {
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(COMMAND_EXCHANGE_NAME, type: ExchangeType.Direct);
                channel.BasicPublish(exchange: COMMAND_EXCHANGE_NAME, routingKey: destination, basicProperties: null, body: command.ToJson().ToByteArray());
            }
        }

        public void BasicPublish(IEvent @event)
        {      
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(EVENT_EXCHANGE_NAME, type: ExchangeType.Fanout);
                channel.BasicPublish(exchange: EVENT_EXCHANGE_NAME, routingKey: string.Empty, basicProperties: null, body: @event.ToJson().ToByteArray());
            }

        }

        public void StartConsuming()
        {
            consumerChannel = connection.CreateModel();
            consumerChannel.ExchangeDeclare(COMMAND_EXCHANGE_NAME, ExchangeType.Direct);
            consumerChannel.ExchangeDeclare(EVENT_EXCHANGE_NAME, ExchangeType.Fanout);

            var queue = consumerChannel.QueueDeclare(queue: queueName);
            consumerChannel.QueueBind(queue, COMMAND_EXCHANGE_NAME, routingKey: queueName);
            consumerChannel.QueueBind(queue, EVENT_EXCHANGE_NAME, routingKey: queueName);

            var consumer = new EventingBasicConsumer(consumerChannel);

            consumerChannel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            consumer.Received += (model, ea) => {
                var messageBody = ea.Body.ToArray().GetPayloadString();
                var args = JsonConvert.DeserializeObject<MessageReceivedEventArgs>(messageBody); 
                MessageReceived.Invoke(args);
            };
            
        }
    }
}