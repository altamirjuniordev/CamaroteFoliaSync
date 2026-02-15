using CamaroteFoliaSync.Domain.ValueObjects;

namespace CamaroteFoliaSync.Domain.Events
{
    public class FoliaoSaiuEvent : DomainEvent
    {
        public PulseiraId PulseiraId { get; }
        public Guid CamaroteId { get; }
        public int LotacaoAtual { get; }

        public FoliaoSaiuEvent(PulseiraId pulseiraId, Guid camaroteId, int lotacaoAtual)
        {
            PulseiraId = pulseiraId;
            CamaroteId = camaroteId;
            LotacaoAtual = lotacaoAtual;
        }
    }
}
