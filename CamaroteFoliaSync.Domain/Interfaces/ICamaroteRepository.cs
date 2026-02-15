using CamaroteFoliaSync.Domain.Entities;

namespace CamaroteFoliaSync.Domain.Interfaces
{
    public interface ICamaroteRepository
    {
        Task<Camarote?> ObterPorIdAsync(Guid id);
        Task<Camarote?> ObterComFolioesAsync(Guid id);
        Task AdicionarAsync(Camarote camarote);
        Task AtualizarAsync(Camarote camarote);
        Task AdicionarRegistroFluxoAsync(RegistroFluxo registro);
    }
}
