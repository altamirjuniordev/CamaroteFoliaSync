using CamaroteFoliaSync.Application.DTOs;
using CamaroteFoliaSync.Application.Interfaces;
using CamaroteFoliaSync.Domain.Entities;
using CamaroteFoliaSync.Domain.Enums;
using CamaroteFoliaSync.Domain.Events;
using CamaroteFoliaSync.Domain.Exceptions;
using CamaroteFoliaSync.Domain.Interfaces;
using CamaroteFoliaSync.Domain.ValueObjects;
using MediatR;

namespace CamaroteFoliaSync.Application.Commands.RegistrarEntrada;

public class RegistrarEntradaHandler : IRequestHandler<RegistrarEntradaCommand, FluxoResponseDto>
{
    private readonly ICamaroteRepository _camaroteRepository;
    private readonly IEventPublisher _eventPublisher;

    public RegistrarEntradaHandler(ICamaroteRepository camaroteRepository, IEventPublisher eventPublisher)
    {
        _camaroteRepository = camaroteRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<FluxoResponseDto> Handle( RegistrarEntradaCommand request, CancellationToken cancellationToken)
    {
        var camarote = await _camaroteRepository.ObterPorIdAsync(request.CamaroteId)
            ?? throw new InvalidOperationException("Camarote não encontrado.");

        var jaPresente = await _camaroteRepository.FoliaoEstaPresenteAsync(request.CamaroteId, request.PulseiraId);
        if (jaPresente)
            throw new InvalidOperationException("Folião com esta pulseira já está dentro do camarote.");

        var lotacaoAtual = await _camaroteRepository.CalcularLotacaoAsync(request.CamaroteId);
        if (lotacaoAtual >= camarote.CapacidadeMaxima)
            throw new CapacidadeExcedidaException(camarote.CapacidadeMaxima, lotacaoAtual);

        var pulseiraId = new PulseiraId(request.PulseiraId);
        var registro = new RegistroFluxo(request.CamaroteId, pulseiraId, TipoFluxo.Entrada);
        await _camaroteRepository.AdicionarRegistroFluxoAsync(registro);

        var novaLotacao = lotacaoAtual + 1;

        await _eventPublisher.PublicarAsync(new FoliaoEntrouEvent(pulseiraId, request.CamaroteId, novaLotacao));

        return new FluxoResponseDto(
            registro.Id,
            request.PulseiraId,
            "Entrada",
            novaLotacao,
            camarote.CapacidadeMaxima,
            registro.DataHora);
    }
}
