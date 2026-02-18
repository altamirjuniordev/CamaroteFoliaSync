using CamaroteFoliaSync.Application.DTOs;
using CamaroteFoliaSync.Domain.Interfaces;
using MediatR;

namespace CamaroteFoliaSync.Application.Queries.ObterLotacao;

public class ObterLotacaoHandler : IRequestHandler<ObterLotacaoQuery, LotacaoDto>
{
    private readonly ICamaroteRepository _camaroteRepository;

    public ObterLotacaoHandler(ICamaroteRepository camaroteRepository)
    {
        _camaroteRepository = camaroteRepository;
    }

    public async Task<LotacaoDto> Handle(ObterLotacaoQuery request, CancellationToken cancellationToken)
    {
        var camarote = await _camaroteRepository.ObterPorIdAsync(request.CamaroteId)
            ?? throw new InvalidOperationException("Camarote não encontrado.");

        var lotacaoAtual = await _camaroteRepository.CalcularLotacaoAsync(request.CamaroteId);

        var percentual = camarote.CapacidadeMaxima > 0
            ? Math.Round((decimal)lotacaoAtual / camarote.CapacidadeMaxima * 100, 2)
            : 0;

        return new LotacaoDto(
            camarote.Id,
            camarote.Nome,
            lotacaoAtual,
            camarote.CapacidadeMaxima,
            percentual);
    }
}