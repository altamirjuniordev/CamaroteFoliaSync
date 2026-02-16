using CamaroteFoliaSync.Domain.Entities;
using CamaroteFoliaSync.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CamaroteFoliaSync.Infrastructure.Persistence.Repositories;

public class CamaroteRepository : ICamaroteRepository
{
    private readonly AppDbContext _context;

    public CamaroteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Camarote?> ObterPorIdAsync(Guid id)
    {
        return await _context.Camarotes.FindAsync(id);
    } 

    public async Task<Camarote?> ObterComFolioesAsync(Guid id)
    {
        return await _context.Camarotes.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AdicionarAsync(Camarote camarote)
    {
        await _context.Camarotes.AddAsync(camarote);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Camarote camarote)
    {
        _context.Camarotes.Update(camarote);
        await _context.SaveChangesAsync();
    }

    public async Task AdicionarRegistroFluxoAsync(RegistroFluxo registro)
    {
        await _context.RegistrosFluxos.AddAsync(registro);
        await _context.SaveChangesAsync();
    }
}
