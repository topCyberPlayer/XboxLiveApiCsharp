namespace XblApp.EventSourcingDemoMininmalAPI.Events.Handler
{
    public class GamerCreatedEventHandler : IDomainEventHandler<GamerCreatedEvent>
    {
        public Task HandleAsync(GamerCreatedEvent domainEvent)
        {
            // Логика обработки события создания геймера
            Console.WriteLine($"Gamer created with ID: {domainEvent.GamerId} and Gamertag: {domainEvent.Gamertag}");
            return Task.CompletedTask;
        }
    }
}
