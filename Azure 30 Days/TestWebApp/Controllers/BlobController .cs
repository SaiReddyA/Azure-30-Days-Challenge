namespace TestWebApp.Controllers;
public class BlobController : Controller
{
    private readonly BlobService _blobService;

    public BlobController(BlobService blobService)
    {
        _blobService = blobService;
    }

    [Route("Upload")]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            await _blobService.UploadAsync(file);
            ViewBag.Message = "Upload successful!";
        }
        return View();
    }

    [Route("Download")]
    public IActionResult Download()
    {
        return View();
    }

    [HttpPost]
    [Route("Download")]
    public async Task<IActionResult> Download(string fileName)
    {
        var stream = await _blobService.DownloadAsync(fileName);
        return File(stream, "application/octet-stream", fileName);
    }
}
