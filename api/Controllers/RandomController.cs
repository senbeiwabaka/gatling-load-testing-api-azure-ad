using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace load_testing_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class RandomController : ControllerBase
    {
        [HttpGet]
        public IActionResult Random()
        {
            Random random = new Random(DateTime.Now.Millisecond);

            return Ok(random.Next());
        }
    }
}