namespace TestWebApp.Controllers
{
    [Route("[controller]")]
    public class AzureFileController : Controller
    {
        private readonly string _connectionString;
        private readonly string _shareName;

        public AzureFileController(IConfiguration config)
        {
            _connectionString = config["AzureFileShare:ConnectionString"];
            _shareName = config["AzureFileShare:ShareName"];
        }

        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            var shareClient = new ShareClient(_connectionString, _shareName);
            await shareClient.CreateIfNotExistsAsync();

            var rootDir = shareClient.GetRootDirectoryClient();
            List<FileItem> files = new List<FileItem>();

            await foreach (ShareFileItem file in rootDir.GetFilesAndDirectoriesAsync())
            {
                if (!file.IsDirectory)
                {
                    files.Add(new FileItem
                    {
                        FileName = file.Name,
                        DownloadUrl = Url.Action("Download", "AzureFile", new { fileName = file.Name })
                    });
                }
            }
            return View(files);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var shareClient = new ShareClient(_connectionString, _shareName);
                await shareClient.CreateIfNotExistsAsync();

                var rootDir = shareClient.GetRootDirectoryClient();
                var fileClient = rootDir.GetFileClient(uploadedFile.FileName);

                using (var stream = uploadedFile.OpenReadStream())
                {
                    await fileClient.CreateAsync(stream.Length);
                    await fileClient.UploadAsync(stream);
                }
            }

            return RedirectToAction("Index");
        }

        [Route("[action]")]
        public async Task<IActionResult> Download(string fileName)
        {
            var shareClient = new ShareClient(_connectionString, _shareName);
            var rootDir = shareClient.GetRootDirectoryClient();
            var fileClient = rootDir.GetFileClient(fileName);

            ShareFileDownloadInfo download = await fileClient.DownloadAsync();
            return File(download.Content, "application/octet-stream", fileName);
        }
    }
}
