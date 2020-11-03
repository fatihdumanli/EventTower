using System.Threading.Tasks;

namespace ServiceTower
{
    public interface IMessageHandler<T> where T: IMessage
    {
        Task Handle(T message);
    }
}