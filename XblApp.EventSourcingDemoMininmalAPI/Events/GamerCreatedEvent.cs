namespace XblApp.EventSourcingDemoMininmalAPI.Events
{
    public class GamerCreatedEvent : IDomainEvent
    {
        public Guid GamerId { get; }
        public string Gamertag { get; }
        public DateTime OccurredOn { get; }

        public GamerCreatedEvent(Guid gamerId, string gamertag)
        {
            GamerId = gamerId;
            Gamertag = gamertag;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
