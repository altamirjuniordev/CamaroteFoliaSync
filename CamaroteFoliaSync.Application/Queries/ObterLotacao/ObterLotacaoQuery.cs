using CamaroteFoliaSync.Application.DTOs;
using MediatR;

namespace CamaroteFoliaSync.Application.Queries.ObterLotacao;

public record ObterLotacaoQuery(Guid CamaroteId) : IRequest<LotacaoDto>;
