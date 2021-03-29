using Microsoft.AspNetCore.Mvc;

namespace BeComfy.Services.Identity.Controllers
{
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
            => Ok("BeComfy Identity Microservice");
    }
}