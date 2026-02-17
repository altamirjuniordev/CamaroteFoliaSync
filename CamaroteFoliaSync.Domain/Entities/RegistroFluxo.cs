using CamaroteFoliaSync.Domain.Enums;
using CamaroteFoliaSync.Domain.ValueObjects;

namespace CamaroteFoliaSync.Domain.Entities
{
    public class RegistroFluxo : Entity<Guid>
    {
        public Guid CamaroteId { get; private set; }
        public PulseiraId PulseiraId { get; private set; }
        public TipoFluxo Tipo { get; private set; }
        public DateTime DataHora { get; private set; }

        public RegistroFluxo(Guid camaroteId, PulseiraId pulseiraId, TipoFluxo tipo) : base(Guid.NewGuid())
        {
            CamaroteId = camaroteId;
            PulseiraId = pulseiraId;
            Tipo = tipo;
            DataHora = DateTime.UtcNow;
        }

        private RegistroFluxo() : base()
        {
            PulseiraId = default!;
        }
    }
}
