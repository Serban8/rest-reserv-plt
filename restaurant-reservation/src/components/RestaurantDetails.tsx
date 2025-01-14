// RestaurantDetails.tsx with full features for Restaurant Page

import React, { useState } from "react";
import { useParams, Link } from "react-router-dom";
import { Typography, Button, Container, Rating, Grid } from "@mui/material";
import { restaurants, seatLayout } from "../data/mockData.ts";
import SeatSelector from "../components/SeatSelector.tsx";

const RestaurantDetails: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const restaurantId = id ? parseInt(id, 10) : null;
  const restaurant = restaurants.find((r) => r.id === restaurantId);
  const [reservationConfirmed, setReservationConfirmed] = useState(false);

  const handleConfirm = (selectedSeats: number[]) => {
    setReservationConfirmed(true);
    alert(
      `Reservation confirmed for seats: ${selectedSeats.join(", ")} at ${restaurant?.name}`
    );
  };

  return (
    <Container>
      {restaurant ? (
        <>
          {/* Restaurant Information */}
          <Typography variant="h3" sx={{ mt: 4 }}>
            {restaurant.name}
          </Typography>
          <Typography variant="body1" sx={{ my: 2 }}>
            {restaurant.description}
          </Typography>

          {/* Seat Map Section */}
          <Typography variant="h5" sx={{ mt: 4, mb: 2 }}>
            Seat Map
          </Typography>
          <SeatSelector onConfirm={handleConfirm} />

          {/* Reservation Confirmation Message */}
          {reservationConfirmed && (
            <Typography variant="h6" color="success.main" sx={{ mt: 4 }}>
              Thank you! Your reservation has been confirmed. A confirmation email will be sent shortly.
            </Typography>
          )}

          {/* Feedback Section */}
          <Typography variant="h6" sx={{ my: 4 }}>
            Leave Feedback
          </Typography>
          <Rating
            name="feedback"
            value={0}
            onChange={(event, newValue) => {
              alert(`Thank you for your feedback! You rated this restaurant ${newValue} stars.`);
            }}
          />
        </>
      ) : (
        <Typography>Restaurant not found.</Typography>
      )}
    </Container>
  );
};

export default RestaurantDetails;
