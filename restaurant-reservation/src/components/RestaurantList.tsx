import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import instance from "../api/axios"; // Axios instance

const RestaurantList: React.FC = () => {
  const [restaurants, setRestaurants] = useState([]);

  useEffect(() => {
    const fetchRestaurants = async () => {
      try {
        const response = await instance.get("/Restaurant/all-restaurants");
        setRestaurants(response.data);
      } catch (error) {
        console.error("Error fetching restaurants:", error);
      }
    };

    fetchRestaurants();
  }, []);

  return (
    <div>
      <h2>Restaurants</h2>
      <ul>
        {restaurants.map((restaurant) => (
          <li key={restaurant.id}>
            <h3>{restaurant.name}</h3>
            <p>{restaurant.description}</p>
            <Link to={`/restaurant/${restaurant.id}`}>
              <button>View Details</button>
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default RestaurantList;
