using NetCore.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace NetCore.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : IntegrationEvent;

        Task Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        //Task Subscribe<T, TH>(string subscriberGroup, int instance)
        //    where T : IntegrationEvent
        //    where TH : IIntegrationEventHandler<T>;

        Task Subscribe(string eventTypeName, string subscriberGroup, int subscriberNumber, string handlerTypeName);

        Task Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;

        //Task SubscribeDynamic<TH>(string eventName)
        //    where TH : IDynamicIntegrationEventHandler;

        //Task UnsubscribeDynamic<TH>(string eventName)
        //    where TH : IDynamicIntegrationEventHandler;
    }
}
