import React, { useState } from "react";
import { Button, Grid, Typography, Container } from "@mui/material";
import { seatLayout } from "../data/mockData.ts";

interface SeatSelectorProps {
  onConfirm: (selectedSeats: number[]) => void;
}

const SeatSelector: React.FC<SeatSelectorProps> = ({ onConfirm }) => {
  const [selectedSeats, setSelectedSeats] = useState<number[]>([]);

  const toggleSeat = (id: number) => {
    setSelectedSeats((prev) =>
      prev.includes(id) ? prev.filter((seat) => seat !== id) : [...prev, id]
    );
  };

  return (
    <Container>
      <Typography variant="h5" sx={{ mb: 2 }}>Select Your Seats</Typography>
      <Grid container spacing={2}>
        {seatLayout.map((seat) => (
          <Grid item key={seat.id}>
            <Button
              variant="contained"
              color={selectedSeats.includes(seat.id) ? "success" : "secondary"}
              onClick={() => toggleSeat(seat.id)}
            >
              {seat.label}
            </Button>
          </Grid>
        ))}
      </Grid>
      <Button
        variant="contained"
        color="primary"
        sx={{ mt: 3 }}
        onClick={() => onConfirm(selectedSeats)}
      >
        Confirm Reservation
      </Button>
    </Container>
  );
};

export default SeatSelector;
