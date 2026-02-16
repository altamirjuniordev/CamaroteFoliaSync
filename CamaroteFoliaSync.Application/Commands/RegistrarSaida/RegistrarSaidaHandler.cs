using CamaroteFoliaSync.Domain.Interfaces;
using CamaroteFoliaSync.Domain.ValueObjects;
using CamaroteFoliaSync.Application.DTOs;
using CamaroteFoliaSync.Application.Interfaces;
using MediatR;


namespace CamaroteFoliaSync.Application.Commands.RegistrarSaida;

public class RegistrarSaidaHandler : IRequestHandler<RegistrarSaidaCommand, FluxoResponseDto>
{
    private readonly ICamaroteRepository _camaroteRepository;
    private readonly IEventPublisher _eventPublisher;

    public RegistrarSaidaHandler( ICamaroteRepository camaroteRepository, IEventPublisher eventPublisher)
    {
        _camaroteRepository = camaroteRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<FluxoResponseDto> Handle( RegistrarSaidaCommand request, CancellationToken cancellationToken)
    {
        var camarote = await _camaroteRepository.ObterComFolioesAsync(request.CamaroteId)
            ?? throw new InvalidOperationException("Camarote não encontrado");

        var pulseiraId = new PulseiraId(request.PulseiraId);
        var registro = camarote.RegistrarSaida(pulseiraId);

        await _camaroteRepository.AtualizarAsync(camarote);
        await _camaroteRepository.AdicionarRegistroFluxoAsync(registro);

        foreach(var evento in camarote.DomainEvents)
        {
            await _eventPublisher.PublicarAsync(evento);
        }

        camarote.LimparEventos();

        return new FluxoResponseDto(
            registro.Id,
            request.PulseiraId,
            "Saida",
                camarote.LotacaoAtual,
                camarote.CapacidadeMaxima,
                registro.DataHora);
    }
}
