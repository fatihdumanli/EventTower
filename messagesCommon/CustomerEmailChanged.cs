using MessageBus;

namespace MessagesCommon
{
    public class CustomerEmailChanged : IEvent
    {
        public string CustomerId { get; set; }   
        public string Email { get; set; }     
    }
}