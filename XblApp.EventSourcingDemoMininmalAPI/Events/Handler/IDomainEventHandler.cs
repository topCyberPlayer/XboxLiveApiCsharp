namespace XblApp.EventSourcingDemoMininmalAPI.Events.Handler
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        Task HandleAsync(T domainEvent);
    }
}
