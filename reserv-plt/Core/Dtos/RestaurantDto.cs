using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class RestaurantDto
    {
        public RestaurantDto(string name, string address, string phoneNumber, string email, string website, string description, string imageUrl, string schedule, Guid? id = null)
        {
            Id = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;
            Description = description;
            ImageUrl = imageUrl;
            Schedule = schedule;
        }

        public Guid? Id { get; set; } = null;
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Schedule { get; set; } // JSON-encoded schedule

        public static RestaurantDto FromRestaurant(Restaurant restaurant)
        {
            return new RestaurantDto(restaurant.Name, restaurant.Address, restaurant.PhoneNumber, restaurant.Email, restaurant.Website, restaurant.Description, restaurant.ImageUrl, restaurant.Schedule, restaurant.Id);
        }

        public static List<RestaurantDto> FromRestaurantList(List<Restaurant> restaurants)
        {
            List<RestaurantDto> restaurantDtos = new List<RestaurantDto>();
            foreach (var restaurant in restaurants)
            {
                restaurantDtos.Add(FromRestaurant(restaurant));
            }
            return restaurantDtos;
        }
    }
}
