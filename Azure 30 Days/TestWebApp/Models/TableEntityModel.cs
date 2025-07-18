namespace TestWebApp.Models;

public class TableEntityModel : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ETag ETag { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public string ETagString
    {
        get => ETag.ToString();
        set => ETag = new ETag(value);
    }
}
