using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QueuePublisher.MessageService;
using System.Collections.Generic;

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
            try
            {
                _messageNotify.NotifyService(content.Content);
                return Ok();
            }
            catch (System.Exception e)
            {

                return BadRequest(JsonConvert.SerializeObject(e));
            }

        }
    }

    public class PostContent
    {
        public string Content { get; set; }
    }
}
