using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QueuePublisher.MessageService;

namespace QueuePublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageQueueConfig _messageQueueConfig;
        private readonly IMessageNotify<string> _messageNotify;

        public MessageController(IOptions<MessageQueueConfig> messageQueueConfig, IMessageNotify<string> messageNotify)
        {
            _messageQueueConfig = messageQueueConfig.Value;
            _messageNotify = messageNotify;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { $"MessageController ok! {JsonConvert.SerializeObject(_messageQueueConfig)}" };
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
