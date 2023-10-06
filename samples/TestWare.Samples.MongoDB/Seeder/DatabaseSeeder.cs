using MongoDB.Bson;
using TestWare.Core;
using TestWare.Engines.MongoDB.Factory;

namespace TestWare.Samples.MongoDB.Seeder;

internal class DatabaseSeeder
{
    private readonly IMongoDbClient _mongoDbClient;
    private const string DATABASE_NAME = "database-example";
    private const string COLLECTION_NAME = "collection-example";

    public DatabaseSeeder()
    {
        _mongoDbClient = ContainerManager.GetTestWareComponent<IMongoDbClient>();
    }

    public void InitializeDatabase()
    {
        _mongoDbClient.DropDatabase(DATABASE_NAME);

        _mongoDbClient.CreateDatabase(DATABASE_NAME);
        _mongoDbClient.CreateCollection(DATABASE_NAME, COLLECTION_NAME);

        _mongoDbClient.InsertOne(new BsonDocument("name", "Diego"), DATABASE_NAME, COLLECTION_NAME);
    }
}
