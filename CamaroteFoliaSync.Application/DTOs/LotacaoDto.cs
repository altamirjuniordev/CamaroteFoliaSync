namespace CamaroteFoliaSync.Application.DTOs;

public record LotacaoDto(
    Guid CamaroteId,
    string NomeCamarote,
    int LotacaoAtual,
    int CapacidadeMaxima,
    decimal PecentualOcupacao);
