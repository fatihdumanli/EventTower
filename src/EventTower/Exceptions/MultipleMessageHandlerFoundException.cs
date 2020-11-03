using System;

namespace EventTower.Exceptions
{
    public class MultipleMessageHandlerFoundException : Exception
    {
        public MultipleMessageHandlerFoundException(string messageType) : 
            base(string.Format("Multiple message handler found for type: {0}. Only one handler per message type allowed.", messageType))
        {
            
        }        
    }
}