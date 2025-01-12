using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace reserv_plt.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("all-restaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            try
            {
                var restaurants = RestaurantDto.FromRestaurantList(await _restaurantService.GetAll());
                return Ok(restaurants);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("restaurant/{id}")]
        public async Task<IActionResult> GetRestaurant(Guid id)
        {
            try
            {
                var restaurant = RestaurantDto.FromRestaurant(await _restaurantService.GetById(id));
                return Ok(restaurant);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
