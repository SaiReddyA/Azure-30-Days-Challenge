using Azure_Learning_Series.Services;
using Microsoft.AspNetCore.Mvc;

namespace Azure_Learning_Series.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretController : ControllerBase
    {
        private readonly KeyVaultService _keyVaultService;

        public SecretController()
        {
            _keyVaultService = new KeyVaultService();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var secret = await _keyVaultService.GetSecretAsync(name);
            return Ok(secret);
        }

    }
}
