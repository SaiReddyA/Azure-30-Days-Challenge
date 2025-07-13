namespace Azure_Learning_Series.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ConfigTestController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ConfigTestController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string value = _config["ConnectionStrings:Connection"];
            return Ok(new { Setting = value });
        }
    }

}
