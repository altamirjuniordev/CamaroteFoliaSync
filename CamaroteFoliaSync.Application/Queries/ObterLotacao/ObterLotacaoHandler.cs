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
        var camarote = await _camaroteRepository.ObterComFolioesAsync(request.CamaroteId)
            ?? throw new InvalidOperationException("Camarote não encontrado.");

        var percentual = camarote.CapacidadeMaxima > 0
            ? Math.Round((decimal)camarote.LotacaoAtual / camarote.CapacidadeMaxima * 100, 2)
            : 0;

        return new LotacaoDto(
            camarote.Id,
            camarote.Nome,
            camarote.LotacaoAtual,
            camarote.CapacidadeMaxima,
            percentual);
    }
}