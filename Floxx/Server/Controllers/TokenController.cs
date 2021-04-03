using System.Threading.Tasks;
using Floxx.Core.DTO.Identity;
using Floxx.Core.Interfaces;
using Floxx.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Floxx.Server.Controllers
{
    [Route("api/identity/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService identityService;
        private readonly ICurrentUserService currentUserService;

        public TokenController(ITokenService identityService, ICurrentUserService currentUserService)
        {
            this.identityService = identityService;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<ActionResult> Get(TokenRequest model)
        {
            var response = await identityService.LoginAsync(model);
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest model)
        {
            var response = await identityService.GetRefreshTokenAsync(model);
            return Ok(response);
        }
    }
}
