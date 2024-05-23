using MongoDB.Bson;

namespace TestWare.Engines.MongoDB.Factory;

public interface IMongoDbClient
{
    void CreateDatabase(string databaseName);

    void DropDatabase(string databaseName);

    void CreateCollection(string databaseName, string collectionName);

    void DropCollection(string databaseName, string collectionName);

    void InsertOne(BsonDocument document, string databaseName, string collectionName);

    void InsertMany(IEnumerable<BsonDocument> documentList, string databaseName, string collectionName);

    void UpdateOne(BsonDocument document, string databaseName, string collectionName);

    void UpdateMany(BsonDocument document, string databaseName, string collectionName);

    void DeleteOne(BsonDocument document, string databaseName, string collectionName);

    void DeleteMany(BsonDocument document, string databaseName, string collectionName);

    Task<long> Count(BsonDocument document, string databaseName, string collectionName);

    Task<List<BsonDocument>> Find(BsonDocument document, string databaseName, string collectionName);

    List<string> GetClientLogs();

    void ClearClientLogs();
}
