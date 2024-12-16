using Microsoft.EntityFrameworkCore;
using reserv_plt.Core.Dtos;
using reserv_plt.DataLayer;
using reserv_plt.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TableService
    {
        private readonly AppDbContext _appDbContext;

        public TableService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<TableDto>> GetAvailableTables()
        {
            var tables = await _appDbContext.Tables
                .Where(t => t.IsAvailable)
                .Select(t => new TableDto(
                    t.Id,
                    t.TableNumber,
                    t.IsAvailable
                ))
                .ToListAsync();
            return tables;
        }

        public async Task<ReservationResponseDto> ReserveTable(ReservationRequestDto request)
        {
            var table = await _appDbContext.Tables
                .FirstOrDefaultAsync(t => t.Id == request.TableId && t.IsAvailable);

            if (table == null)
                return null;

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

            _appDbContext.Reservations.Add(reservation);
            await _appDbContext.SaveChangesAsync();

            var response = new ReservationResponseDto(
                reservation.Id,
                table.Id,
                reservation.CustomerName,
                reservation.ReservationDate,
                reservation.IsConfirmed
            );

            return response;
        }
    }
}
