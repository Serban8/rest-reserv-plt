using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace reserv_plt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpPost("add-table")]
        public async Task<StatusCodeResult> AddTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpDelete("remove-table")]
        public async Task<StatusCodeResult> RemoveTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpPut("update-table")]
        public async Task<StatusCodeResult> UpdateTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpPost("confirm-reservation")]
        public async Task<StatusCodeResult> ManuallyConfirmReservation()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpPost("cancel-reservation")]
        public async Task<StatusCodeResult> ManuallyCancelReservation()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
