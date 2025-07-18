namespace TestWebApp.Controllers;

[Route("[controller]")]
public class TableController : Controller
{
    private readonly TableStorageService _service;

    public TableController(TableStorageService service)
    {
        _service = service;
    }

    [Route("[controller]")]
    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(data);
    }

    [Route("[action]")]
    public IActionResult Create() => View();

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create(TableEntityModel model)
    {
        model.PartitionKey = "DemoPartition";
        model.RowKey = Guid.NewGuid().ToString();
        await _service.AddEntityAsync(model);
        return RedirectToAction("Index");
    }

    [Route("[action]")]
    public async Task<IActionResult> Edit(string pk, string rk)
    {
        var entity = await _service.GetAsync(pk, rk);
        return View(entity);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Edit(TableEntityModel model)
    {
        await _service.UpdateEntityAsync(model);
        return RedirectToAction("Index");
    }

    [Route("[action]")]
    public async Task<IActionResult> Delete(string pk, string rk)
    {
        await _service.DeleteEntityAsync(pk, rk);
        return RedirectToAction("Index");
    }
}

