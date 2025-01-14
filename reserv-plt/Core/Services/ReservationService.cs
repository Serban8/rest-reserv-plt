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
        private readonly ConfirmationRepository _confirmationRepository;
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public ReservationService(ReservationRepository reservationRepository, TableRepository tableRepository, ConfirmationRepository confirmationRepository, UserService userService, EmailService emailService)
        {
            _reservationRepository = reservationRepository;
            _tableRepository = tableRepository;
            _confirmationRepository = confirmationRepository;
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<ReservationResponseDto> ReserveTable(ReservationRequestDto request)
        {
            //Check if the table is available
            var table = await _tableRepository.GetByIdAsync(request.TableID) ?? throw new Exception("Table not found");
            if (table.Reservations != null && table.Reservations.Any(r =>
            {
                TimeSpan timeSpan = r.ReservationDate - request.ReservationDate;
                return Math.Abs(timeSpan.TotalMinutes) < 90;
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


            var email = await _userService.GetEmail(request.UserID);
            var firstName = await _userService.GetFirstName(request.UserID);

            await _emailService.SendReservationEmailAsync(
                recipientEmail: email,  
                recipientName: firstName,    
                reservationId: reservation.Id
            );

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

        public async Task FinishReservation(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId) ?? throw new Exception("Reservation not found");
            reservation.IsFinished = true;

            await _reservationRepository.SaveAllChangesAsync();
        }

        public async Task ConfirmReservation(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId) ?? throw new Exception("Reservation not found");
            reservation.Confirmation.IsConfirmed = true;

            await _reservationRepository.SaveAllChangesAsync();
        }

        public async Task DeleteReservation(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId) ?? throw new Exception("Reservation not found");
            _confirmationRepository.DeleteAsync(reservation.Confirmation);
            _reservationRepository.DeleteAsync(reservation);
            await _reservationRepository.SaveAllChangesAsync();
        }
    }
}
