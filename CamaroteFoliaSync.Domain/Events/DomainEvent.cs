namespace CamaroteFoliaSync.Domain.Events
{
    public abstract class DomainEvent
    {
        public Guid EventId { get; }
        public DateTime OcurredAt { get; }

        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
            OcurredAt = DateTime.UtcNow;
        }
    }
}
