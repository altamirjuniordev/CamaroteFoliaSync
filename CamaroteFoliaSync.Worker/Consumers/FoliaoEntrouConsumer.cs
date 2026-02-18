
using CamaroteFoliaSync.Domain.Events;
using MassTransit;

namespace CamaroteFoliaSync.Worker.Consumers;

public class FoliaoEntrouConsumer : IConsumer<FoliaoEntrouEvent>
{
    private readonly ILogger<FoliaoEntrouConsumer> _logger;

    public FoliaoEntrouConsumer(ILogger<FoliaoEntrouConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<FoliaoEntrouEvent> context)
    {
        var evento = context.Message;

        _logger.LogInformation(
            "🎉 ENTRADA: Pulseira {PulseiraId} entrou no Camarote {CamaroteId}. Lotação: {Lotacao}",
            evento.PulseiraId.Valor,
            evento.CamaroteId,
            evento.LotacaoAtual);

        return Task.CompletedTask;
    }
}