using CamaroteFoliaSync.Application.DTOs;
using MediatR;

namespace CamaroteFoliaSync.Application.Commands.RegistrarEntrada;

public record RegistrarEntradaCommand(
    Guid CamaroteId,
    string PulseiraId
    ) : IRequest<FluxoResponseDto>;

