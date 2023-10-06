using TestWare.Engines.MongoDB.Configuration;

namespace TestWare.Engines.MongoDB.Factory;

internal class ClientFactory
{
    public static IMongoDbClient Create(Capabilities capabilities)
    {
        if (capabilities == null || capabilities.ConnectionString == null) throw new ArgumentNullException(nameof(capabilities));
        return new MongoDbClient(capabilities.ConnectionString);
    }
}
