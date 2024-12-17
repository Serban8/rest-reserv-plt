using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Dtos;
using Core.Services;

namespace reserv_plt.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly TableService _tableService;

        public TableController(TableService tableService)
        {
            _tableService = tableService;
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpGet("all-tables")]
        public async Task<IActionResult> GetAvailableTables()
        {
            var tables = await _tableService.GetAvailableTables();
            return Ok(tables);
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reserve-table")]
        public async Task<IActionResult> ReserveTable([FromBody] ReservationRequestDto request)
        {
            var response = await _tableService.ReserveTable(request);

            return Ok(response);
        }
    }
}
