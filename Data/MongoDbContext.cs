public class MongoDbContext
{
    private readonly IMongoDatabase _db;

    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
        _db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
    }

    public IMongoCollection<Produto> Produtos => _db.GetCollection<Produto>("Produtos");
}
