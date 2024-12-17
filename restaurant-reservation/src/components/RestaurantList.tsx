import React from "react";
import { Card, CardContent, Typography, Grid } from "@mui/material";
import { Link } from "react-router-dom";
import { restaurants } from "../data/mockData.ts";

const RestaurantList: React.FC = () => {
  return (
    <Grid container spacing={3} padding={3}>
      {restaurants.map((restaurant) => (
        <Grid item xs={12} sm={6} md={4} key={restaurant.id}>
          <Card sx={{ boxShadow: 3 }}>
            <CardContent>
              <Typography variant="h5">{restaurant.name}</Typography>
              <Typography color="text.secondary">{restaurant.description}</Typography>
              <Link to={`/restaurant/${restaurant.id}`} style={{ textDecoration: "none" }}>
                <Typography color="primary" sx={{ mt: 2 }}>View Details</Typography>
              </Link>
            </CardContent>
          </Card>
        </Grid>
      ))}
    </Grid>
  );
};

export default RestaurantList;
