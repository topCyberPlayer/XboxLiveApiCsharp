using XblApp.EventSourcingDemoMininmalAPI.Events;

namespace XblApp.EventSourcingDemoMininmalAPI.Entities
{
    /// <summary>
    /// 3. Базовый класс для Entity (чтобы хранить события)
    /// </summary>
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
