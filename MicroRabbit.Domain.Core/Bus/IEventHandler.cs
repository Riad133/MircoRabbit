using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Events;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventHandler<in IEvent>: IEventHandler
    where IEvent : Event
    {
        Task Handle(IEvent @event);
    }

    public interface IEventHandler
    {
        
    }
}