using System;

namespace MessageBus
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Type { get; private set; }
        public object Payload { get; private set; }
        public DateTime PublishDate { get; private set; }
        public MessageReceivedEventArgs(string type, object payload, DateTime publishDate)
        {
            this.Type = type;
            this.Payload = payload;            
            this.PublishDate = publishDate;
        }
    }
}