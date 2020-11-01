using System;
using RabbitMQ.Client;

namespace MessageBus
{
    public class ConnectionProvider
    {
        private static bool _isConnected = false;
        private static IConnection _connection;        
        
        static ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

        private static void TryConnect() 
        {
            _connection = factory.CreateConnection();
            _isConnected = true;
            Console.WriteLine("connected!");
        }

        public static IConnection Get()
        {
            if(!_isConnected) {
                TryConnect();                
            }

            return _connection;
        }
    }
}