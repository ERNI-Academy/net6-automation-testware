using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace TestWare.Engines.MongoDB.Factory;

internal class MongoDbClient : MongoClient, IMongoDbClient
{
    private readonly MongoClient mongoClient;

    private List<string> clientLogs;

    public MongoDbClient(string connectionString)
    {
        clientLogs = new List<string>();
        var mongoConnectionUrl = new MongoUrl(connectionString);
        var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

        mongoClientSettings.ClusterConfigurator = cb =>
        {
            cb.Subscribe<CommandStartedEvent>(e =>
            {
                clientLogs.Add($"{e.CommandName} - {e.Command.ToJson()}");
            });
        };

        mongoClient = new MongoClient(mongoClientSettings);
    }

    public void CreateDatabase(string databaseName)
    {
        GetDatabase(databaseName);
    }

    public void DropDatabase(string databaseName)
    {
        mongoClient.DropDatabase(databaseName);
    }

    public void CreateCollection(string databaseName, string collectionName) 
    {
        var database = GetDatabase(databaseName);
        database.CreateCollection(collectionName);
    }

    public void DropCollection(string databaseName, string collectionName)
    {
        var database = GetDatabase(databaseName);
        database.DropCollection(collectionName);
    }

    public async void InsertOne(BsonDocument document, string databaseName, string collectionName) 
    {
        var database = GetDatabase(databaseName);
        var collection = GetCollection(database, collectionName);

        await collection.InsertOneAsync(document);
    }

    public async void InsertMany(IEnumerable<BsonDocument> documentList, string databaseName, string collectionName)
    {
        var database = GetDatabase(databaseName);
        var collection = GetCollection(database, collectionName);

        await collection.InsertManyAsync(documentList);
    }

    public void UpdateOne(BsonDocument document, string databaseName, string collectionName)
    {
        var database = GetDatabase(databaseName);
        var collection = GetCollection(database, collectionName);

        throw new NotImplementedException("Pending to implement");
    }

    public void UpdateMany(BsonDocument document, string databaseName, string collectionName)
    {
        var database = GetDatabase(databaseName);
        var collection = GetCollection(database, collectionName);

        throw new NotImplementedException("Pending to implement");
    }

    public async void DeleteOne(BsonDocument document, string databaseName, string collectionName)
    {
        var database = GetDatabase(databaseName);
        var collection = GetCollection(database, collectionName);

        await collection.DeleteOneAsync(document);
    }

    public async void DeleteMany(BsonDocument document, string databaseName, string collectionName)
    {
        var database = GetDatabase(databaseName);
        var collection = GetCollection(database, collectionName);

        await collection.DeleteManyAsync(document);
    }

    public async Task<long> Count(BsonDocument document, string databaseName, string collectionName)
    {
        var database = GetDatabase(databaseName);
        var collection = GetCollection(database, collectionName);

        var count = await collection.CountDocumentsAsync(document);

        return count;
    }

    public async Task<List<BsonDocument>> Find(BsonDocument document, string databaseName, string collectionName)
    {
        var database = GetDatabase(databaseName);
        var collection = GetCollection(database, collectionName);

        var list = await collection.Find(document).ToListAsync();

        return list;
    }

    public List<string> GetClientLogs() 
    {
        return clientLogs;
    }

    public void ClearClientLogs()
    {
        clientLogs = new List<string>();
    }

    private IMongoDatabase GetDatabase(string databaseName)
    {
       return mongoClient.GetDatabase(databaseName);
    }

    private static IMongoCollection<BsonDocument> GetCollection(IMongoDatabase database, string collectionName)
    {
        return database.GetCollection<BsonDocument>(collectionName);
    }
}

