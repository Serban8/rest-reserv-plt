using Core.Dtos;
using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly TableRepository _tableRepository;

        public ReservationService(ReservationRepository reservationRepository, TableRepository tableRepository)
        {
            _reservationRepository = reservationRepository;
            _tableRepository = tableRepository;
        }

        public async Task<ReservationResponseDto> ReserveTable(ReservationRequestDto request)
        {
            //Check if the table is available
            var table = await _tableRepository.GetByIdAsync(request.TableID) ?? throw new Exception("Table not found");
            if (table.Reservations != null && table.Reservations.Any(r =>
            {
                TimeSpan timeSpan = r.ReservationDate - request.ReservationDate;
                return timeSpan.TotalMinutes < 90;
            }))
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

            return ReservationResponseDto.FromReservation(reservation);
        }

        public async Task<List<ReservationResponseDto>> GetAllForTable(Guid tableId)
        {
            var reservations = (await _reservationRepository.GetAllAsync()).Where(r => r.TableID == tableId).ToList();

            return reservations.Select(r => ReservationResponseDto.FromReservation(r)).ToList();
        }

        public async Task<List<ReservationResponseDto>> GetAllForRestaurant(Guid restaurantId)
        {
            var reservations = (await _reservationRepository.GetAllAsync()).Where(r => r.RestaurantId == restaurantId).ToList();

            return reservations.Select(r => ReservationResponseDto.FromReservation(r)).ToList();
        }
    }
}
