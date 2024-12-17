import React from "react";
import { Grid, Card, CardContent, Typography, Button, Container } from "@mui/material";
import { Link } from "react-router-dom";
import { restaurants } from "../data/mockData.ts"; // Sample data

const HomePage: React.FC = () => {
  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h3" gutterBottom>
        Restaurants
      </Typography>

      <Grid container spacing={4}>
        {restaurants.map((restaurant) => (
          <Grid item xs={12} sm={6} md={4} key={restaurant.id}>
            <Card sx={{ boxShadow: 3 }}>
              <CardContent>
                <Typography variant="h5">{restaurant.name}</Typography>
                <Typography variant="body2" color="textSecondary">
                  {restaurant.description}
                </Typography>
                <Link to={`/restaurant/${restaurant.id}`} style={{ textDecoration: "none" }}>
                  <Button variant="contained" color="primary" sx={{ mt: 2 }}>
                    View Details
                  </Button>
                </Link>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Container>
  );
};

export default HomePage;
