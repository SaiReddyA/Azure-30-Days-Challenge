namespace TestWebApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class FileItem
    {
        public string FileName { get; set; }
        public string DownloadUrl { get; set; }
    }

}
