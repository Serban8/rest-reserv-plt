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
        private readonly ReservationService _reservationService;

        public TableController(TableService tableService, ReservationService reservationService)
        {
            _tableService = tableService;
            _reservationService = reservationService;
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
                var response = await _reservationService.ReserveTable(request);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
