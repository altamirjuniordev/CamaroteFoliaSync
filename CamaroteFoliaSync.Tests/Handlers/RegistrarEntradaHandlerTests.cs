using CamaroteFoliaSync.Application.Commands.RegistrarEntrada;
using CamaroteFoliaSync.Application.Interfaces;
using CamaroteFoliaSync.Domain.Entities;
using CamaroteFoliaSync.Domain.Events;
using CamaroteFoliaSync.Domain.Exceptions;
using CamaroteFoliaSync.Domain.Interfaces;
using Moq;

namespace CamaroteFoliaSync.Tests.Handlers;

public class RegistrarEntradaHandlerTests
{
    private readonly Mock<ICamaroteRepository> _repositoryMock;
    private readonly Mock<IEventPublisher> _eventPublisherMock;
    private readonly RegistrarEntradaHandler _handler;

    public RegistrarEntradaHandlerTests()
    {
        _repositoryMock = new Mock<ICamaroteRepository>();
        _eventPublisherMock = new Mock<IEventPublisher>();
        _handler = new RegistrarEntradaHandler(_repositoryMock.Object, _eventPublisherMock.Object);
    }

    [Fact]
    public async Task Handle_DeveRegistrarEntrada_QuandoDadosValidos()
    {
        var camaroteId = Guid.NewGuid();
        var camarote = new Camarote("Camarote VIP", 100);
        typeof(Entity<Guid>).GetProperty("Id")!.SetValue(camarote, camaroteId);

        _repositoryMock.Setup(r => r.ObterPorIdAsync(camaroteId)).ReturnsAsync(camarote);
        _repositoryMock.Setup(r => r.FoliaoEstaPresenteAsync(camaroteId, "PULSEIRA-001")).ReturnsAsync(false);
        _repositoryMock.Setup(r => r.CalcularLotacaoAsync(camaroteId)).ReturnsAsync(10);

        var command = new RegistrarEntradaCommand(camaroteId, "PULSEIRA-001");

        var resultado = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Entrada", resultado.TipoFluxo);
        Assert.Equal(11, resultado.LotacaoAtual);
        Assert.Equal("PULSEIRA-001", resultado.PulseiraId);
        _repositoryMock.Verify(r => r.AdicionarRegistroFluxoAsync(It.IsAny<RegistroFluxo>()), Times.Once);
        _eventPublisherMock.Verify(e => e.PublicarAsync(It.IsAny<FoliaoEntrouEvent>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DeveLancarExcecao_QuandoFoliaoJaPresente()
    {
        var camaroteId = Guid.NewGuid();
        var camarote = new Camarote("Camarote VIP", 100);
        typeof(Entity<Guid>).GetProperty("Id")!.SetValue(camarote, camaroteId);

        _repositoryMock.Setup(r => r.ObterPorIdAsync(camaroteId)).ReturnsAsync(camarote);
        _repositoryMock.Setup(r => r.FoliaoEstaPresenteAsync(camaroteId, "PULSEIRA-001")).ReturnsAsync(true);

        var command = new RegistrarEntradaCommand(camaroteId, "PULSEIRA-001");

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_DeveLancarCapacidadeExcedida_QuandoLotacaoMaxima()
    {
        var camaroteId = Guid.NewGuid();
        var camarote = new Camarote("Camarote VIP", 100);
        typeof(Entity<Guid>).GetProperty("Id")!.SetValue(camarote, camaroteId);

        _repositoryMock.Setup(r => r.ObterPorIdAsync(camaroteId)).ReturnsAsync(camarote);
        _repositoryMock.Setup(r => r.FoliaoEstaPresenteAsync(camaroteId, "PULSEIRA-001")).ReturnsAsync(false);
        _repositoryMock.Setup(r => r.CalcularLotacaoAsync(camaroteId)).ReturnsAsync(100);

        var command = new RegistrarEntradaCommand(camaroteId, "PULSEIRA-001");

        await Assert.ThrowsAsync<CapacidadeExcedidaException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
}
