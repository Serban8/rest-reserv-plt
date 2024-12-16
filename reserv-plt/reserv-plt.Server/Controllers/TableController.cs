using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reserv_plt.Core.Dtos;
using reserv_plt.DataLayer.Models; 
using reserv_plt.DataLayer;
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

        // GET: api/Table
        [HttpGet]
        public async Task<IActionResult> GetAvailableTables()
        {
            var tables = await _tableService.GetAvailableTables();
            return Ok(tables);
        }

        // POST: api/Table/Reserve
        [HttpPost("Reserve")]
        public async Task<IActionResult> ReserveTable([FromBody] ReservationRequestDto request)
        {
            var response = await _tableService.ReserveTable(request);

            return Ok(response);
        }
    }
}
