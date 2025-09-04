using XblApp.EventSourcingDemoMininmalAPI.Events;

namespace XblApp.EventSourcingDemoMininmalAPI.Entities
{
    /// <summary>
    /// 4. Entity Gamer, которая генерирует событие
    /// </summary>
    public class Gamer : Entity
    {
        public Guid Id { get; private set; }
        public string Gamertag { get; private set; }

        public Gamer() { }

        public Gamer(string Gamertag)
        {
            Id = Guid.NewGuid();
            this.Gamertag = Gamertag;

            AddDomainEvent(new GamerCreatedEvent(Id, Gamertag));
        }
    }
}
