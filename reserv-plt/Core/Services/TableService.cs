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

        public async Task<IEnumerable<TableDto>> GetAvailableTables()
        {
            var tables = await _tableRepository.GetAllAsync();
            var tableDtos = tables
                .Select(t => new TableDto(
                    t.Id,
                    t.TableNumber,
                    true
                ));

            return tableDtos;
        }

        public async Task<ReservationResponseDto> ReserveTable(ReservationRequestDto request)
        {

            return null;
        }
    }
}
