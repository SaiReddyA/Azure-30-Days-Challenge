
namespace TestWebApp.Services
{

    public class QueueService
    {
        private readonly QueueClient _queueClient;

        public QueueService(IConfiguration configuration)
        {
            var connStr = configuration["AzureStorage:ConnectionString"];
            var queueName = configuration["AzureStorage:QueueName"];
            _queueClient = new QueueClient(connStr, queueName);
            _queueClient.CreateIfNotExists();
        }

        public async Task SendMessageAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
        }

        public async Task<string> ReadMessageAsync()
        {
            var msg = await _queueClient.ReceiveMessageAsync();
            return msg.Value?.MessageText;
        }
    }
}
