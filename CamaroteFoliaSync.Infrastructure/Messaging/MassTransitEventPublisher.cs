using CamaroteFoliaSync.Application.Interfaces;
using CamaroteFoliaSync.Domain.Events;
using MassTransit;

namespace CamaroteFoliaSync.Infrastructure.Messaging;

 public class MassTransitEventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublicarAsync<TEvent>(TEvent evento) where TEvent : DomainEvent
    {
        await _publishEndpoint.Publish(evento);
    }
}