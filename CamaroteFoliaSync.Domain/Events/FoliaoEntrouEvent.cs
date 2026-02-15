using CamaroteFoliaSync.Domain.ValueObjects;

namespace CamaroteFoliaSync.Domain.Events
{
    public class FoliaoEntrouEvent : DomainEvent
    {
        public PulseiraId PulseiraId { get; }
        public Guid CamaroteId { get; }
        public int LotacaoAtual { get; }

        public FoliaoEntrouEvent(PulseiraId pulseiraId, Guid camaroteId, int lotacaoAtual)
        {
            PulseiraId = pulseiraId;
            CamaroteId = camaroteId;
            LotacaoAtual = lotacaoAtual;
        }
    }
}
