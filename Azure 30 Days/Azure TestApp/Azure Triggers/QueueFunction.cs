using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
namespace Azure_TestApp;

public class QueueFunction
{
    private readonly ILogger _logger;

    public QueueFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<QueueFunction>();
    }

    [Function("QueueFunction")]
    public void Run([QueueTrigger("sample-queue", Connection = "AzureWebJobsStorage")] string queueItem)
    {
        _logger.LogInformation($"Queue trigger function processed: {queueItem}");
    }
}
