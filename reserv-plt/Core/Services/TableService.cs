    using Microsoft.EntityFrameworkCore;
using Core.Dtos;
using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;
using Core.Dtos.AddDtos;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Core.Services
{
    public class TableService
    {
        private readonly TableRepository _tableRepository;
        private readonly ReservationRepository _reservationRepository;

        public TableService(TableRepository tableRepository, ReservationRepository reservationRepository)
        {
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
        }

        //Get all the tables in a restaurant and set their availability for a specific date
        public async Task<IEnumerable<TableDto>> GetAllAvailableTables(Guid restaurantId, DateTime forDate)
        {
            var tables = (await _tableRepository.GetAllAsync()).Where(t => t.RestaurantID == restaurantId).ToList();
            var tableDtos = new List<TableDto>();

            foreach (var table in tables)
            {
                bool available = true;
                if (table.Reservations != null)
                    available = table.Reservations.All(r =>
                    {
                        TimeSpan timeSpan = r.ReservationDate - forDate;
                        return timeSpan.TotalMinutes > 90;
                    });
                tableDtos.Add(TableDto.FromTable(table, available));
            }

            return tableDtos;
        }

        public async Task<TableDto> Add(TableAddDto tableDto)
        {
            var table = new Table
            {
                TableNumber = tableDto.TableNumber,
                Seats = tableDto.Seats,
                Position = tableDto.Position,
                RestaurantID = tableDto.RestaurantID
            };

            table = await _tableRepository.AddAsync(table);
            await _tableRepository.SaveAllChangesAsync();

            return TableDto.FromTable(table, true);
        }

        public async Task Delete(Guid tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId) ?? throw new Exception("Table not found");
            _tableRepository.DeleteAsync(table);
            await _tableRepository.SaveAllChangesAsync();
        }

        public async Task<List<ReservationResponseDto>> GetAllReservations(Guid tableId)
        {
            var reservations = (await _reservationRepository.GetAllAsync()).Where(r => r.TableID == tableId).ToList();

            return reservations.Select(r => ReservationResponseDto.FromReservation(r)).ToList();
        }
    }
}
