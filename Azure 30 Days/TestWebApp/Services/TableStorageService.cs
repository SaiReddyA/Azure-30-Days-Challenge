namespace TestWebApp.Services;

public class TableStorageService
{
    private readonly TableClient _tableClient;

    public TableStorageService(IConfiguration configuration)
    {
        var connStr = configuration["AzureStorage:ConnectionString"];
        var tableName = configuration["AzureStorage:TableName"];
        _tableClient = new TableClient(connStr, tableName);
        _tableClient.CreateIfNotExists();
    }

    public async Task AddEntityAsync(TableEntityModel entity)
    {
        await _tableClient.AddEntityAsync(entity);
    }

    public async Task<List<TableEntityModel>> GetAllAsync()
    {
        return _tableClient.Query<TableEntityModel>().ToList();
    }

    public async Task<TableEntityModel> GetAsync(string partitionKey, string rowKey)
    {
        var response = await _tableClient.GetEntityAsync<TableEntityModel>(partitionKey, rowKey);
        return response.Value;
    }

    public async Task UpdateEntityAsync(TableEntityModel entity)
    {
        await _tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Replace);
    }

    public async Task DeleteEntityAsync(string partitionKey, string rowKey)
    {
        await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
    }
}


