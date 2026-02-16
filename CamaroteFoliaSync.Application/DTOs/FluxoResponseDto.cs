namespace CamaroteFoliaSync.Application.DTOs;
    public record FluxoResponseDto(
        Guid RegistroId,
        string PulseiraId,
        string TipoFluxo,
        int LotacaoAtual,
        int CapacidadeMaxima,
        DateTime DataHora
        );
   