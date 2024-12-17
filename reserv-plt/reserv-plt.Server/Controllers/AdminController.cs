using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace reserv_plt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpPost("add-table")]
        public async Task<StatusCodeResult> AddTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpDelete("remove-table")]
        public async Task<StatusCodeResult> RemoveTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPut("update-table")]
        public async Task<StatusCodeResult> UpdateTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("confirm-reservation")]
        public async Task<StatusCodeResult> ManuallyConfirmReservation()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("cancel-reservation")]
        public async Task<StatusCodeResult> ManuallyCancelReservation()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
