using CamaroteFoliaSync.Domain.Enums;
using CamaroteFoliaSync.Domain.ValueObjects;

namespace CamaroteFoliaSync.Domain.Entities
{
    public class RegistroFluxo : Entity<Guid>
    {
        public PulseiraId PulseiraId { get; private set; }
        public TipoFluxo Tipo { get; private set; }
        public DateTime DataHora { get; private set; }

        public RegistroFluxo(PulseiraId pulseiraId, TipoFluxo tipo) : base(Guid.NewGuid())
        {
            PulseiraId = pulseiraId;
            Tipo = tipo;
            DataHora = DateTime.UtcNow;
        }
    }
}
