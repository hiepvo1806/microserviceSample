using Microsoft.AspNetCore.Mvc;
using PlusService.Model;
using System;
using System.Threading;

namespace PlusService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlusController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Plus service type";
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlusModel value)
        {
            try
            {
                var result = value.X + value.Y;
                Thread.Sleep(5000);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.ToString());
            }
        }
    }
}
