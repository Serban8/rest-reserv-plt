using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reserv_plt.Core.Dtos;
using reserv_plt.DataLayer.Models; 
using reserv_plt.DataLayer;

namespace reserv_plt.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TableController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Table
        [HttpGet]
        public async Task<IActionResult> GetAvailableTables()
        {
            var tables = await _context.Tables
                .Where(t => t.IsAvailable)
                .Select(t => new TableDto(
                    t.Id,
                    t.TableNumber,
                    t.IsAvailable
                ))
                .ToListAsync();

            return Ok(tables);
        }

        // POST: api/Table/Reserve
        [HttpPost("Reserve")]
        public async Task<IActionResult> ReserveTable([FromBody] ReservationRequestDto request)
        {
            var table = await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == request.TableId && t.IsAvailable);

            if (table == null)
                return BadRequest("Table is unavailable or does not exist.");

            // Reserve table
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                TableId = table.Id,
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                ReservationDate = request.DateAndTime,
                IsConfirmed = false
            };

            table.IsAvailable = false;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var response = new ReservationResponseDto(
                reservation.Id,
                table.Id,
                reservation.CustomerName,
                reservation.ReservationDate,
                reservation.IsConfirmed
            );

            return Ok(response);
        }
    }
}
