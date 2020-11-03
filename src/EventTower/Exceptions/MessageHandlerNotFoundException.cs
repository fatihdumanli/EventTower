using System;

namespace EventTower.Exceptions
{
    public class MessageHandlerNotFoundException : Exception
    {
        public MessageHandlerNotFoundException(string messageType) : 
            base(string.Format("Message handler not found for the message type: {0}", messageType))
        {
            
        }        
    }
}