using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace AspNetCoreWebApi6.Controllers
{
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShowMyTokenController : ControllerBase
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        
        private readonly ILogger<ShowMyTokenController> _logger;

        public ShowMyTokenController(ILogger<ShowMyTokenController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [HttpGet("token")]
        public string Get()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            _logger.Log(LogLevel.Information, $"Getting Token {token}");
            return token;
        }
    }
}