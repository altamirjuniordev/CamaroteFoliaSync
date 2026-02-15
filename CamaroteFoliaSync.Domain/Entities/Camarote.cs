using CamaroteFoliaSync.Domain.Enums;
using CamaroteFoliaSync.Domain.Events;
using CamaroteFoliaSync.Domain.Exceptions;
using CamaroteFoliaSync.Domain.ValueObjects;

namespace CamaroteFoliaSync.Domain.Entities
{
    public class Camarote : Entity<Guid>
    {
        private readonly List<DomainEvent> _domainEvents = new();
        private readonly HashSet<PulseiraId> _folioesPresentes = new();

        public string Nome { get; private set; }
        public int CapacidadeMaxima { get; private set; }
        public int LotacaoAtual => _folioesPresentes.Count;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public Camarote(string nome, int capacidadeMaxima) : base(Guid.NewGuid())
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome do camarote é obrigatório.", nameof(nome));

            if (capacidadeMaxima <= 0)
                throw new ArgumentException("Capacidade deve ser maior que zero.", nameof(capacidadeMaxima));

            Nome = nome;
            CapacidadeMaxima = capacidadeMaxima;
        }

        public RegistroFluxo RegistrarEntrada(PulseiraId pulseiraId)
        {
            if (_folioesPresentes.Contains(pulseiraId))
                throw new InvalidOperationException("Folião já está dentro do camarote.");

            if (LotacaoAtual >= CapacidadeMaxima)
                throw new CapacidadeExcedidaException(CapacidadeMaxima, LotacaoAtual);

            _folioesPresentes.Add(pulseiraId);

            var registro = new RegistroFluxo(pulseiraId, TipoFluxo.Entrada);
            _domainEvents.Add(new FoliaoEntrouEvent(pulseiraId, Id, LotacaoAtual));

            return registro;
        }

        public RegistroFluxo RegistrarSaida(PulseiraId pulseiraId)
        {
            if (!_folioesPresentes.Contains(pulseiraId))
                throw new InvalidOperationException("Folião não está no camarote.");

            _folioesPresentes.Remove(pulseiraId);

            var registro = new RegistroFluxo(pulseiraId, TipoFluxo.Saida);
            _domainEvents.Add(new FoliaoSaiuEvent(pulseiraId, Id, LotacaoAtual));

            return registro;
        }

        public void LimparEventos()
        {
            _domainEvents.Clear();
        }
    }
}
