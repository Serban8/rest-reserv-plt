import React, { useState } from "react";
import instance from "../api/axios";

const RestaurantReservationsPage: React.FC = () => {
  const [restaurantId, setRestaurantId] = useState("");
  const [reservations, setReservations] = useState([]);
  const [loading, setLoading] = useState(false);

  // Fetch reservations for a restaurant
  const fetchReservations = async () => {
    try {
      setLoading(true);
      const response = await instance.get("/Admin/get-all-reservations", {
        params: { restaurantId },
      });
      setReservations(response.data);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching reservations:", error);
      setLoading(false);
    }
  };

  // Confirm a reservation
  const confirmReservation = async (reservationId: string) => {
    try {
      const response = await instance.put("/Admin/confirm-reservation", null, {
        params: { reservationId },
      });
      console.log("Reservation confirmed:", response.data);
      alert("Reservation confirmed!");
      fetchReservations(); // Refresh reservations
    } catch (error) {
      console.error("Error confirming reservation:", error);
      alert("Failed to confirm the reservation.");
    }
  };

  // Cancel a reservation
  const cancelReservation = async (reservationId: string) => {
    try {
      await instance.delete("/Admin/cancel-reservation", {
        params: { reservationId },
      });
      console.log("Reservation canceled.");
      alert("Reservation canceled!");
      fetchReservations(); // Refresh reservations
    } catch (error) {
      console.error("Error canceling reservation:", error);
      alert("Failed to cancel the reservation.");
    }
  };

  return (
    <div>
      <h2>Restaurant Reservations</h2>
      <div>
        <label>Enter Restaurant ID:</label>
        <input
          type="text"
          value={restaurantId}
          onChange={(e) => setRestaurantId(e.target.value)}
          placeholder="Restaurant ID"
        />
        <button onClick={fetchReservations} disabled={!restaurantId || loading}>
          Fetch Reservations
        </button>
      </div>

      {loading && <p>Loading reservations...</p>}

      {reservations.length > 0 ? (
        <table border="1">
          <thead>
            <tr>
              <th>Reservation ID</th>
              <th>Date</th>
              <th>Number of People</th>
              <th>Customer Name</th>
              <th>Table</th>
              <th>Confirmed</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {reservations.map((reservation: any) => (
              <tr key={reservation.reservationId}>
                <td>{reservation.reservationId}</td>
                <td>{reservation.reservationDate}</td>
                <td>{reservation.numberOfPeople}</td>
                <td>{reservation.customerName}</td>
                <td>{reservation.tableNumber}</td>
                <td>{reservation.isConfirmed ? "Yes" : "No"}</td>
                <td>
                  <button
                    onClick={() => confirmReservation(reservation.reservationId)}
                    style={{
                      marginRight: "10px",
                      background: "green",
                      color: "white",
                    }}
                    disabled={reservation.isConfirmed}
                  >
                    Confirm
                  </button>
                  <button
                    onClick={() => cancelReservation(reservation.reservationId)}
                    style={{ background: "red", color: "white" }}
                  >
                    Cancel
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        !loading && <p>No reservations found.</p>
      )}
    </div>
  );
};

export default RestaurantReservationsPage;
