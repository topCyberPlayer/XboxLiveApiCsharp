namespace XblApp.EventSourcingDemoMininmalAPI.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
