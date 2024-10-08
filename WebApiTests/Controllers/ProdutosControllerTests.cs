public class ProdutosControllerTests
{
    private readonly ProdutosController _controller;
    private readonly Mock<MongoDbContext> _mockContext;

    public ProdutosControllerTests()
    {
        _mockContext = new Mock<MongoDbContext>();
        _controller = new ProdutosController(_mockContext.Object);
    }

    [Fact]
    public async Task Get_ReturnsAllProdutos()
    {
        // Arrange
        var mockProdutos = new List<Produto> { new Produto { Id = "1", Nome = "Teste", Preco = 10M } };
        _mockContext.Setup(db => db.Produtos.Find(It.IsAny<FilterDefinition<Produto>>()).ToListAsync())
                    .ReturnsAsync(mockProdutos);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnProdutos = Assert.IsType<List<Produto>>(okResult.Value);
        Assert.Single(returnProdutos);
    }
}
