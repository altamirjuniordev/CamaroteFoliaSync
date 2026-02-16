using CamaroteFoliaSync.Domain.Events;

namespace CamaroteFoliaSync.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublicarAsync<TEvent>(TEvent evento) where TEvent : DomainEvent;
    }
}
