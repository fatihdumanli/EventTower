using System;
using System.Net.Sockets;
using MessageBus.Extensions;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace MessageBus
{   
    public delegate void Notify(string str);

    public class RabbitMQAdapter
    {
        public event Notify MessageReceived;
        private const string EXCHANGE_NAME = "MyServiceBus";
        private int retryCount = 3;
        private bool _isConnected = false;
        private IConnection _connection;    
        private string queueName;
        private IModel consumerChannel;

        private IConnection connection 
        {
            get
            {
                if(!_isConnected)
                {
                    TryConnect();
                }

                return _connection;
            }
        }

        public RabbitMQAdapter(string endpointName)
        {
            if(string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentException("Endpoint name is not valid.");
            }

            queueName = endpointName;
        }
        
        ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

        private void TryConnect() 
        {
            var policy = RetryPolicy.Handle<SocketException>().Or<BrokerUnreachableException>()
                .WaitAndRetry(retryCount, op => TimeSpan.FromSeconds(Math.Pow(2, op)), (ex, time) => {
                    Console.WriteLine("Couldn't connect to RabbitMQ server...");
                });

            policy.Execute(() => {
                _connection = factory.CreateConnection();
                _isConnected = true;
                Console.WriteLine("connected!");
            });
        }


        public void BasicPublish(ICommand command, string destination)
        {
            
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(EXCHANGE_NAME, type: ExchangeType.Direct);
                channel.BasicPublish(exchange: EXCHANGE_NAME, routingKey: destination, basicProperties: null, body: command.ToJson().ToByteArray());
                Console.WriteLine("Command is sent to the destination {0} Content: {1}", destination, command.ToJson());
            }

        }

        public void BasicPublish(IEvent @event)
        {      
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(EXCHANGE_NAME, type: ExchangeType.Fanout);
                channel.BasicPublish(exchange: EXCHANGE_NAME, routingKey: string.Empty, basicProperties: null, body: @event.ToJson().ToByteArray());
                Console.WriteLine("Event is published.");
            }

        }

        public void StartConsuming()
        {
            consumerChannel = connection.CreateModel();
            consumerChannel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct);
            var queue = consumerChannel.QueueDeclare(queue: queueName);
            consumerChannel.QueueBind(queue, EXCHANGE_NAME, routingKey: queueName);

            var consumer = new EventingBasicConsumer(consumerChannel);

            consumerChannel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            consumer.Received += (model, ea) => {
                MessageReceived.Invoke("testArg");
            };
            
        }


    }
}