using NetCore.EventBus.Events;
using System.Threading.Tasks;

namespace NetCore.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> 
        //: IIntegrationEventHandler 
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
        Task Handle(object @event);
    }
}
