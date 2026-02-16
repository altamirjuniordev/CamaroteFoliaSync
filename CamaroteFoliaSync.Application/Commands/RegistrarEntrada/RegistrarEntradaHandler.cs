using CamaroteFoliaSync.Application.DTOs;
using CamaroteFoliaSync.Application.Interfaces;
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
        var camarote = await _camaroteRepository.ObterComFolioesAsync(request.CamaroteId) 
            ?? throw new InvalidOperationException("Camarote não encontrado.");

        var pulseiraId = new PulseiraId(request.PulseiraId);
        var registro = camarote.RegistrarEntrada(pulseiraId);

        await _camaroteRepository.AtualizarAsync(camarote);
        await _camaroteRepository.AdicionarRegistroFluxoAsync(registro);

        foreach (var evento in camarote.DomainEvents)
        {
            await _eventPublisher.PublicarAsync(evento);
        }

        camarote.LimparEventos();

        return new FluxoResponseDto(
            registro.Id,
            request.PulseiraId,
            "Entrada",
            camarote.LotacaoAtual,
            camarote.CapacidadeMaxima,
            registro.DataHora);
    }
}
