using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Redis.PubSub.Services;

namespace API.Redis.PubSub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisPublisherController : ControllerBase
    {
        private readonly RedisPublisherService redisPublisherService;

        public RedisPublisherController(RedisPublisherService redisPublisherService)
        {
            this.redisPublisherService = redisPublisherService;
        }

        [HttpGet("Publish/{message}")]
        public async Task<IActionResult> GetValue([FromRoute]string message)
        {
            await redisPublisherService.PublishAsync(message);
            return Ok("Publish success");
        }
    }
}
