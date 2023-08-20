using FluentAssertions.Execution;
using MongoDB.Bson;
using TestWare.Core;
using TestWare.Engines.MongoDB.Factory;

namespace TestWare.Samples.MongoDB.StepDefinitions;

[Binding]
public class DatabaseStepDefinitions
{
    private readonly IMongoDbClient _mongoDbClient;

    public DatabaseStepDefinitions()
    {
        _mongoDbClient = ContainerManager.GetTestWareComponent<IMongoDbClient>();
    }

    [When(@"the following document is inserted in '([^']*)' collection at '([^']*)' database")]
    public void TheFollowingDocumentIsInsertedInCollectionAtDatabase(string collectionName, string databaseName, Table table)
    {
        var value = table.Rows[0]["NAME"].ToString();

        _mongoDbClient.InsertOne(new BsonDocument("name", value), databaseName, collectionName);
    }

    [When(@"the following document is deleted in '([^']*)' collection at '([^']*)' database")]
    public void TheFollowingDocumentIsDeletedInCollectionAtDatabase(string collectionName, string databaseName, Table table)
    {
        var value = table.Rows[0]["NAME"].ToString();

        _mongoDbClient.DeleteOne(new BsonDocument("name", value), databaseName, collectionName);
    }


    [Given(@"the following document is saved in '([^']*)' collection at '([^']*)' database")]
    [Then(@"the following document is saved in '([^']*)' collection at '([^']*)' database")]
    public void TheFollowingDocumentIsSavedInCollectionAtDatabase(string collectionName, string databaseName, Table table)
    {
        var value = table.Rows[0]["NAME"].ToString();

        var result = _mongoDbClient.Find(new BsonDocument("name", value), databaseName, collectionName).Result;
        
        using (new AssertionScope()) 
        {
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result[0].GetValue("name").RawValue.Should().Be(value);
        }
    }

    [Then(@"no documents are saved in '([^']*)' collection at '([^']*)' database with values")]
    public void ThenNoDocumentsAreSavedInCollectionAtDatabaseWithValues(string collectionName, string databaseName, Table table)
    {
        var value = table.Rows[0]["NAME"].ToString();

        var result = _mongoDbClient.Find(new BsonDocument("name", value), databaseName, collectionName).Result;

        result.Count.Should().Be(0);
    }

}
