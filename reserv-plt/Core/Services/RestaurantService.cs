using Core.Dtos;
using DataLayer.Models;
using DataLayer.Repositories;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Core.Services
{
    public class RestaurantService
    {
        private readonly RestaurantRepository _restaurantRepository;

        public RestaurantService(RestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<Restaurant> Add(RestaurantDto restaurant)
        {
            try
            {
                //validate that schedule is the correct format (JSON)

                /*
                 Example correct schedule:
                    { "Monday": { "Open": "08:00", "Close": "20:00" }, "Tuesday": { "Open": "08:00", "Close": "20:00" }, "Wednesday": { "Open": "08:00", "Close": "20:00" }, "Thursday": { "Open": "08:00", "Close": "20:00" }, "Friday": { "Open": "08:00", "Close": "20:00" }, "Saturday": { "Open": "08:00", "Close": "20:00" }, "Sunday": { "Open": "08:00", "Close": "20:00" } }
                 Same line but with escape characters for '"':
                    { \"Monday\": { \"Open\": \"08:00\", \"Close\": \"20:00\" }, \"Tuesday\": { \"Open\": \"08:00\", \"Close\": \"20:00\" }, \"Wednesday\": { \"Open\": \"08:00\", \"Close\": \"20:00\" }, \"Thursday\": { \"Open\": \"08:00\", \"Close\": \"20:00\" }, \"Friday\": { \"Open\": \"08:00\", \"Close\": \"20:00\" }, \"Saturday\": { \"Open\": \"08:00\", \"Close\": \"20:00\" }, \"Sunday\": { \"Open\": \"08:00\", \"Close\": \"20:00\" } }
                 */
                var schedule = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(restaurant.Schedule);

                if (schedule == null || schedule.Count != 7)
                {
                    throw new Exception("Invalid schedule format");
                }

                //expect to have 7 days of the week with opening and closing hours
                List<string> days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                foreach (var day in days)
                {
                    if (!schedule.ContainsKey(day))
                    {
                        throw new Exception("Invalid schedule format");
                    }

                    var daySchedule = schedule[day];
                    if (daySchedule.Count != 2)
                    {
                        throw new Exception("Invalid schedule format");
                    }

                    var openingHour = daySchedule["Open"].ToString();
                    var closingHour = daySchedule["Close"].ToString();

                    if (string.IsNullOrEmpty(openingHour) || string.IsNullOrEmpty(closingHour))
                    {
                        throw new Exception("Invalid schedule format");
                    }
                }


                Restaurant newRestaurant = new Restaurant
                {
                    Name = restaurant.Name,
                    Address = restaurant.Address,
                    PhoneNumber = restaurant.PhoneNumber,
                    Email = restaurant.Email,
                    Website = restaurant.Website,
                    Description = restaurant.Description,
                    ImageUrl = restaurant.ImageUrl,
                    Schedule = restaurant.Schedule
                };

                await _restaurantRepository.AddAsync(newRestaurant);
                await _restaurantRepository.SaveAllChangesAsync();

                return newRestaurant;
            }
            catch (JsonException)
            {
                throw new Exception("Invalid schedule format");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Restaurant>> GetAll()
        {
            return await _restaurantRepository.GetAllAsync() ?? throw new Exception("No restaurants found");
        }

        public async Task<Restaurant> GetById(Guid id)
        {
            return await _restaurantRepository.GetByIdAsync(id) ?? throw new Exception("Restaurant not found");
        }

        public async Task Delete(Guid id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id) ?? throw new Exception("Restaurant not found");
            
            _restaurantRepository.DeleteAsync(restaurant);
            await _restaurantRepository.SaveAllChangesAsync();
        }

        public async Task<List<TableDto>> GetAllTables(Guid restaurantId)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId) ?? throw new Exception("Restaurant not found");

            List<TableDto> tables = [];
            
            foreach (var table in restaurant.Tables)
            {
                tables.Add(TableDto.FromTable(table, true));
            }

            return tables;
        }
    }
}
