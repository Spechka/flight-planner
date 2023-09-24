using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleaningController : ControllerBase
    {
        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            return Ok();
        }
    }
}
