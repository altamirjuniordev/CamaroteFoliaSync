namespace CamaroteFoliaSync.Domain.Exceptions
{
    public class CapacidadeExcedidaException : Exception
    {
        public int CapacidadeMaxima { get; }
        public int LotacaoAtual { get; }

        public CapacidadeExcedidaException(int capacidadeMaxima, int lotacaoAtual)
        {
            CapacidadeMaxima = capacidadeMaxima;
            LotacaoAtual = lotacaoAtual;
        }
    }
}
