using System.Threading.Tasks;

namespace EventTower
{
    public interface IMessageHandler<T> where T: IMessage
    {
        Task Handle(T message);
    }
}