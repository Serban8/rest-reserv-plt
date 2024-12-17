using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace reserv_plt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<StatusCodeResult> Register()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("login")]
        public async Task<StatusCodeResult> Login()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

    }
}
