﻿using Core.Dtos;
using Core.Dtos.AddDtos;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace reserv_plt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;
        private readonly TableService _tableService;
        private readonly ReservationService _reservationService;

        public AdminController(RestaurantService restaurantService, TableService tableService, ReservationService reservationService)
        {
            _restaurantService = restaurantService;
            _tableService = tableService;
            _reservationService = reservationService;
        }

        /// <summary>
        /// Add a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpPost("add-restaurant")]
        public async Task<IActionResult> AddRestaurant([FromBody] RestaurantDto restaurant)
        {
            var r = RestaurantDto.FromRestaurant(await _restaurantService.Add(restaurant));

            return Ok(r);

        }

        /// <summary>
        /// Remove a restaurant by id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("remove-restaurant")]
        public async Task<IActionResult> RemoveRestaurant(Guid id)
        {
            try
            {
                await _restaurantService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adds a table to a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpPost("add-table")]
        public async Task<IActionResult> AddTable(TableAddDto table)
        {
            try 
            {
                var retTable = await _tableService.Add(table);
                return Ok(retTable);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Removes a table from a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpDelete("remove-table")]
        public async Task<IActionResult> RemoveTable(Guid id)
        {
            try
            {
                await _tableService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get all reservations for a restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [HttpGet("get-all-reservations")]
        public async Task<IActionResult> GetAllReservations(Guid restaurantId)
        {
            return Ok(await _reservationService.GetAllForRestaurant(restaurantId));
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpPost("confirm-reservation")]
        public async Task<IActionResult> ManuallyConfirmReservation(Guid reservationId)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpPost("cancel-reservation")]
        public async Task<IActionResult> ManuallyCancelReservation(Guid reservationId)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
