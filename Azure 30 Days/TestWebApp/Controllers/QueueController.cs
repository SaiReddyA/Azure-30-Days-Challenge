namespace TestWebApp.Controllers;

[Route("[controller]")]
public class QueueController : Controller
{
    private readonly QueueService _queueService;

    public QueueController(QueueService queueService)
    {
        _queueService = queueService;
    }

    public IActionResult Index() => View();

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Send(string message)
    {
        await _queueService.SendMessageAsync(message);
        ViewBag.Msg = "Message sent to queue!";
        return View("Index");
    }

    [Route("[action]")]
    public async Task<IActionResult> Read()
    {
        var msg = await _queueService.ReadMessageAsync();
        ViewBag.ReadMsg = msg ?? "No messages found.";
        return View("Index");
    }
}
