using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace QueuePublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "MessageController ok!" };
        }

        [HttpPost]
        public ActionResult<IEnumerable<string>> Post([FromBody] PostContent content)
        {
            return new[] { $"MessageController HttpPost! : {JsonConvert.SerializeObject(content)}" };
        }
    }

    public class PostContent
    {
        public string Content { get; set; }
    }
}
