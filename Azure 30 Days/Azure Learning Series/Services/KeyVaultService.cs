namespace Azure_Learning_Series.Services
{
    public class KeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService()
        {
            var keyVaultUrl = "https://<your-keyvault-name>.vault.azure.net/";
            _secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
            return secret.Value;
        }
    }
}
