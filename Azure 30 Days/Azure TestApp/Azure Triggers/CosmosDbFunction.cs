using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
namespace Azure_TestApp;

public class CosmosDbFunction
{
    private readonly ILogger _logger;

    public CosmosDbFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CosmosDbFunction>();
    }

    [Function("CosmosDbFunction")]
    public void Run([CosmosDBTrigger(
        databaseName: "MyDatabase",
        containerName: "MyContainer",
        Connection = "CosmosDbConnection",
        LeaseContainerName = "leases",
        CreateLeaseContainerIfNotExists = true)] IReadOnlyList<dynamic> documents)
    {
        foreach (var doc in documents)
        {
            _logger.LogInformation($"CosmosDB Trigger - Document: {doc}");
        }
    }
}
