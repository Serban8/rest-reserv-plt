import React from "react";
import { useParams } from "react-router-dom";
import SeatSelector from "../components/SeatSelector.tsx";

const ReservationPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();

  const handleConfirm = (seats: number[]) => {
    alert(`Seats reserved for restaurant ID ${id}: ${seats.join(", ")}`);
  };

  return <SeatSelector onConfirm={handleConfirm} />;
};

export default ReservationPage;
