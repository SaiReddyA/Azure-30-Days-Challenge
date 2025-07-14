using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
namespace Azure_TestApp;

public class ServiceBusQueueFunction
{
    private readonly ILogger _logger;

    public ServiceBusQueueFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ServiceBusQueueFunction>();
    }

    [Function("ServiceBusQueueFunction")]
    public void Run([ServiceBusTrigger("my-queue", Connection = "ServiceBusConnection")] string myQueueItem)
    {
        _logger.LogInformation($"Service Bus queue trigger function received: {myQueueItem}");
    }
}
