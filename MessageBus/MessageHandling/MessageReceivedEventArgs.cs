using System;

namespace MessageBus
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Type { get; private set; }
        public object Payload { get; private set; }
        public MessageReceivedEventArgs(string type, object payload)
        {
            this.Type = type;
            this.Payload = payload;            
        }
    }
}