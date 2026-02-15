namespace CamaroteFoliaSync.Domain.ValueObjects
{
    public record PulseiraId
    {
        public string Valor { get; }

        public PulseiraId(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                throw new ArgumentNullException(nameof(valor));
            
            Valor = valor;
        }
    }
}
