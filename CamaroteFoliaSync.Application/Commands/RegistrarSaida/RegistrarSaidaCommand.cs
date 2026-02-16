using CamaroteFoliaSync.Application.DTOs;
using MediatR;

namespace CamaroteFoliaSync.Application.Commands.RegistrarSaida;

public record RegistrarSaidaCommand(
    Guid CamaroteId,
    string PulseiraId) : IRequest<FluxoResponseDto>;

