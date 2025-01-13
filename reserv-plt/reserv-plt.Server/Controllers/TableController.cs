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
        /// Get all of the tables for a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpGet("all-tables")]
        public async Task<IActionResult> GetAllTables(Guid restaurantId, DateTime forDate)
        {
            var tables = await _tableService.GetAllAvailableTables(restaurantId, forDate);
            return Ok(tables);
        }

        /// <summary>
        /// Reserve a table
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reserve-table")]
        public async Task<IActionResult> ReserveTable([FromBody] ReservationRequestDto request)
        {
            try
            {
                var response = await _tableService.ReserveTable(request);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
