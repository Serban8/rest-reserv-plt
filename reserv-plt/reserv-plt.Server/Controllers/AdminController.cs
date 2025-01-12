using Core.Dtos;
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

        public AdminController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
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
        public async Task<IActionResult> AddTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Removes a table from a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpDelete("remove-table")]
        public async Task<IActionResult> RemoveTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Updates a table in a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpPut("update-table")]
        public async Task<IActionResult> UpdateTable()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet("reservations")]
        public async Task<IActionResult> GetReservations()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Manually confirm a reservation
        /// </summary>
        /// <returns></returns>
        [HttpPost("confirm-reservation")]
        public async Task<IActionResult> ManuallyConfirmReservation()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Manually cancel a reservation
        /// </summary>
        /// <returns></returns>
        [HttpPost("cancel-reservation")]
        public async Task<IActionResult> ManuallyCancelReservation()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
