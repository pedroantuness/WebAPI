[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly MongoDbContext _context;

    public ProdutosController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> Get()
    {
        var produtos = await _context.Produtos.Find(_ => true).ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetById(string id)
    {
        var produto = await _context.Produtos.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (produto == null) return NotFound();
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Produto produto)
    {
        await _context.Produtos.InsertOneAsync(produto);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, Produto produto)
    {
        var result = await _context.Produtos.ReplaceOneAsync(p => p.Id == id, produto);
        if (result.MatchedCount == 0) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var result = await _context.Produtos.DeleteOneAsync(p => p.Id == id);
        if (result.DeletedCount == 0) return NotFound();
        return NoContent();
    }
}
