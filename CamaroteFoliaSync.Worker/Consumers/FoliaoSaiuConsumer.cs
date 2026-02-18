
using CamaroteFoliaSync.Domain.Events;
using MassTransit;

namespace CamaroteFoliaSync.Worker.Consumers;

public class FoliaoSaiuConsumer : IConsumer<FoliaoSaiuEvent>
{
    private readonly ILogger<FoliaoSaiuConsumer> _logger;

    public FoliaoSaiuConsumer(ILogger<FoliaoSaiuConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<FoliaoSaiuEvent> context)
    {
        var evento = context.Message;

        _logger.LogInformation(
            "👋 SAÍDA: Pulseira {PulseiraId} saiu do Camarote {CamaroteId}. Lotação: {Lotacao}",
            evento.PulseiraId.Valor,
            evento.CamaroteId,
            evento.LotacaoAtual);

        return Task.CompletedTask;
    }
}