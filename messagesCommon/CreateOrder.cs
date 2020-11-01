using MessageBus;

namespace MessagesCommon
{
    public class CreateOrder : ICommand
    {
        public string CustomerId { get; set; }        
    }
}