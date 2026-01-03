using Microsoft.AspNetCore.Mvc;

namespace StockTracker.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthContoller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is running");
        }
    }
}
