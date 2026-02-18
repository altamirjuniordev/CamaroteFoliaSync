using CamaroteFoliaSync.Domain.Entities;

namespace CamaroteFoliaSync.Domain.Interfaces
{
    public interface ICamaroteRepository
    {
        Task<Camarote?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Camarote camarote);
        Task AdicionarRegistroFluxoAsync(RegistroFluxo registro);
        Task<int> CalcularLotacaoAsync(Guid camaroteId);
        Task<bool> FoliaoEstaPresenteAsync(Guid camaroteId, string pulseiraId);
    }
}
