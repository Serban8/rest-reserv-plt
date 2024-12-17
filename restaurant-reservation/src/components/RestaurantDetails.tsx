import React from "react";
import { useParams, Link } from "react-router-dom";
import { Typography, Button, Container } from "@mui/material";
import { restaurants } from "../data/mockData.ts";

const RestaurantDetails: React.FC = () => {
  const { id } = useParams<{ id: string }>();

  // Ensure `id` is defined before parsing
  const restaurantId = id ? parseInt(id, 10) : null;
  const restaurant = restaurants.find((r) => r.id === restaurantId);

  return (
    <Container>
      {restaurant ? (
        <>
          <Typography variant="h3" sx={{ mt: 4 }}>{restaurant.name}</Typography>
          <Typography variant="body1" sx={{ my: 2 }}>{restaurant.description}</Typography>
          <Link to={`/restaurant/${id}/reserve`} style={{ textDecoration: "none" }}>
            <Button variant="contained" color="primary">Reserve a Table</Button>
          </Link>
        </>
      ) : (
        <Typography>Restaurant not found.</Typography>
      )}
    </Container>
  );
};

export default RestaurantDetails;
