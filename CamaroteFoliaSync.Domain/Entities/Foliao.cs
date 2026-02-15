using CamaroteFoliaSync.Domain.ValueObjects;

namespace CamaroteFoliaSync.Domain.Entities
{
    public class Foliao : Entity<PulseiraId>
    {
        public DateTime DataRegistro { get; private set; }

        public Foliao(PulseiraId pulseiraId) : base(pulseiraId)
        {
            DataRegistro = DateTime.UtcNow;
        }
    }
}
