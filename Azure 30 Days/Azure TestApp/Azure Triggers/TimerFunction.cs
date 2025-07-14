using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Management.ContainerRegistry.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
namespace Azure_TestApp;
public class TimerFunction
{
    private readonly ILogger _logger;

    public TimerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TimerFunction>();
    }

    [Function("TimerFunction")]
    public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation($"Timer function executed at: {DateTime.UtcNow}");
    }
}
