namespace CamaroteFoliaSync.Domain.Entities
{
    public class Camarote : Entity<Guid>
    {
        public string Nome { get; private set; }
        public int CapacidadeMaxima { get; private set; }

        public Camarote(string nome, int capacidadeMaxima) : base(Guid.NewGuid())
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome do camarote é obrigatório.", nameof(nome));

            if (capacidadeMaxima <= 0)
                throw new ArgumentException("Capacidade deve ser maior que zero.", nameof(capacidadeMaxima));

            Nome = nome;
            CapacidadeMaxima = capacidadeMaxima;
        }

        // Construtor para EF Core
        private Camarote() : base()
        {
            Nome = default!;
        }
    }
}
