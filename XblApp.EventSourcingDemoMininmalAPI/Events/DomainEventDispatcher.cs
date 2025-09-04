using System.Reflection;
using XblApp.EventSourcingDemoMininmalAPI.Events.Handler;

namespace XblApp.EventSourcingDemoMininmalAPI.Events
{
    public class DomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handlers = _serviceProvider.GetServices(handlerType);
                
                foreach (var handler in handlers)
                {
                    MethodInfo? method = handlerType.GetMethod("HandleAsync");
                    
                    if (method != null)
                    {
                        await (Task)method.Invoke(handler, new object[] { domainEvent })!;
                    }
                }
            }
        }
    }
}
