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
                    available = table.Reservations.All(r => r.ReservationDate != forDate);
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

        public async Task<ReservationResponseDto> ReserveTable(ReservationRequestDto request)
        {
            //Check if the table is available
            var table = await _tableRepository.GetByIdAsync(request.TableID) ?? throw new Exception("Table not found");
            if (table.Reservations != null && table.Reservations.Any(r => r.ReservationDate == request.ReservationDate))
            {
                throw new Exception("Table is already reserved for that date and time");
            }

           //check if the table has enough seats
            if (table.Seats < request.NumberOfPeople)
            {
                throw new Exception("Table does not have enough seats");
            }

            //Create the reservation
            var reservation = new Reservation
            {
                ReservationDate = request.ReservationDate,
                NumberOfPeople = request.NumberOfPeople,
                RestaurantId = table.RestaurantID,
                TableID = request.TableID,
                UserID = request.UserID
            };

            reservation = await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveAllChangesAsync();

            return new ReservationResponseDto(reservation.Id, reservation.TableID, table.TableNumber, request.UserID.ToString(), reservation.ReservationDate, reservation.Confirmation.IsConfirmed);
        }

        public async Task<List<Reservation>> GetAllReservations(Guid tableId)
        {
            return (await _reservationRepository.GetAllAsync()).Where(r => r.TableID == tableId).ToList();
        }
    }
}
