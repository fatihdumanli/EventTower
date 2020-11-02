using System.Threading.Tasks;

namespace SimpleMessageBus
{
    public interface IMessageHandler<T> where T: IMessage
    {
        Task Handle(T message);
    }
}