using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;

namespace MultipleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Plus service type";
        }

        [HttpPost]
        public IActionResult Post([FromBody] MultipleModel value)
        {
            try
            {
                var result = value.X * value.Y;
                Thread.Sleep(5000);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.ToString());
            }
        }
    }
    public class MultipleModel
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

}
