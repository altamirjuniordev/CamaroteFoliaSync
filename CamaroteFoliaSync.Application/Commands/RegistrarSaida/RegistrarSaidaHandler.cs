using CamaroteFoliaSync.Application.DTOs;
using CamaroteFoliaSync.Application.Interfaces;
using CamaroteFoliaSync.Domain.Entities;
using CamaroteFoliaSync.Domain.Enums;
using CamaroteFoliaSync.Domain.Events;
using CamaroteFoliaSync.Domain.Interfaces;
using CamaroteFoliaSync.Domain.ValueObjects;
using MediatR;

namespace CamaroteFoliaSync.Application.Commands.RegistrarSaida;

public class RegistrarSaidaHandler : IRequestHandler<RegistrarSaidaCommand, FluxoResponseDto>
{
    private readonly ICamaroteRepository _camaroteRepository;
    private readonly IEventPublisher _eventPublisher;

    public RegistrarSaidaHandler(ICamaroteRepository camaroteRepository, IEventPublisher eventPublisher)
    {
        _camaroteRepository = camaroteRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<FluxoResponseDto> Handle( RegistrarSaidaCommand request, CancellationToken cancellationToken)
    {
        var camarote = await _camaroteRepository.ObterPorIdAsync(request.CamaroteId)
            ?? throw new InvalidOperationException("Camarote não encontrado.");

        var estaPresente = await _camaroteRepository.FoliaoEstaPresenteAsync(request.CamaroteId, request.PulseiraId);
        if (!estaPresente)
            throw new InvalidOperationException("Folião não está no camarote.");

        var pulseiraId = new PulseiraId(request.PulseiraId);
        var registro = new RegistroFluxo(request.CamaroteId, pulseiraId, TipoFluxo.Saida);
        await _camaroteRepository.AdicionarRegistroFluxoAsync(registro);

        var lotacaoAtual = await _camaroteRepository.CalcularLotacaoAsync(request.CamaroteId);

        await _eventPublisher.PublicarAsync(new FoliaoSaiuEvent(pulseiraId, request.CamaroteId, lotacaoAtual));

        return new FluxoResponseDto(
            registro.Id,
            request.PulseiraId,
            "Saida",
            lotacaoAtual,
            camarote.CapacidadeMaxima,
            registro.DataHora);
    }
}
