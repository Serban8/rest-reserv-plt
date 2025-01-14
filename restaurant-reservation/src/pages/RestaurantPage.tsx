import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import instance from "../api/axios";

const RestaurantPage: React.FC = () => {
  const { id } = useParams();
  const [restaurant, setRestaurant] = useState(null);
  const navigate = useNavigate();

  const handleReserveTable = () => {
    navigate(`/restaurant/${id}/reserve`);
  };
  useEffect(() => {
    const fetchRestaurant = async () => {
      try {
        console.log(id)
        const response = await instance.get(`/Restaurant/restaurant/${id}`);
        setRestaurant(response.data);
      } catch (error) {
        console.error("Error fetching restaurant details:", error);
      }
    };

    fetchRestaurant();
  }, [id]);

  return (
    <div>
      {restaurant ? (
        <>
          <h1>{restaurant.name}</h1>
          <p>{restaurant.description}</p>
          <p>Address: {restaurant.address}</p>
          <p>Phone: {restaurant.phoneNumber}</p>
          <p>Email: {restaurant.email}</p>
          <p>Website: <a href={restaurant.website}>{restaurant.website}</a></p>
          <img src={restaurant.imageUrl} alt={`${restaurant.name}`} width="300" />
          <button onClick={handleReserveTable}>Reserve a Table</button>
          <h3>Schedule:</h3>
          <ul>
            {Object.entries(restaurant.schedule).map(([day, times]) => (
              <li key={day}>
                {day}: {times.Open} - {times.Close}
              </li>
            ))}
          </ul>
        </>
      ) : (
        <p>Loading restaurant details...</p>
      )}
    </div>
  );
};

export default RestaurantPage;
