using TestWare.Engines.MongoDB.Configuration;

namespace TestWare.Engines.MongoDB.Factory;

internal class ClientFactory
{
    public static IMongoDbClient Create(Capabilities capabilities)
    {
        return new MongoDbClient(capabilities.ConnectionString);
    }
}
