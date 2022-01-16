using API.Redis.Cache.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Redis.Cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICacheService cacheService;
        public HomeController(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        [HttpGet("GetValue/{key}")]
        public async Task<IActionResult> GetValue([FromRoute] string key)
        {
            var value = await cacheService.GetCacheValueAsync(key);
            if (value is not null)
            {
                return Ok(value);
            }
            return NotFound();
        }

        [HttpGet("SetValue/{key}/{value}")]
        public async Task<IActionResult> SetValue([FromRoute] string key, string value)
        {
            await cacheService.SetCacheValueAsync(key, value);
            return Ok(key);
        }
    }
}
