using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
namespace Azure_TestApp;
public class BlobFunction
{
    private readonly ILogger _logger;

    public BlobFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<BlobFunction>();
    }

    [Function("BlobFunction")]
    public void Run([BlobTrigger("sample-container/{name}", Connection = "AzureWebJobsStorage")] Stream blobStream, string name)
    {
        _logger.LogInformation($"Blob trigger function processed blob\n Name: {name} \n Size: {blobStream.Length} bytes");
    }
}
