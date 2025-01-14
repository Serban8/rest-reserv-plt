import React, { useState, useEffect } from "react";
import instance from "../api/axios";

const UserReservationsPage: React.FC = () => {
  console.log("First Name from localStorage:", localStorage.getItem("firstName"));
  const firstName = localStorage.getItem("userId"); // Retrieve user's first name from localStorage
  const userId = localStorage.getItem("userId"); // Retrieve user ID from localStorage

  const [reservations, setReservations] = useState([]);
  const [loading, setLoading] = useState(false);
  const [feedbacks, setFeedbacks] = useState<{ [key: string]: number }>({}); // Store feedback ratings for reservations

  // Fetch all restaurants and user reservations
  const fetchUserReservations = async () => {
    try {
      if (!firstName) {
        console.error("First name is not available in localStorage.");
        return;
      }

      setLoading(true);

      // Step 1: Fetch all restaurants
      const restaurantsResponse = await instance.get("/Restaurant/all-restaurants");
      const restaurants = restaurantsResponse.data;

      // Step 2: Fetch all reservations for each restaurant
      const userReservations: any[] = [];
      for (const restaurant of restaurants) {
        const reservationsResponse = await instance.get("/Admin/get-all-reservations", {
          params: { restaurantId: restaurant.id },
        });

        // Step 3: Filter reservations for the current user based on firstName
        const userSpecificReservations = reservationsResponse.data.filter(
          (reservation: any) => reservation.customerName === firstName
        );

        userReservations.push(...userSpecificReservations);
      }

      setReservations(userReservations);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching user reservations:", error);
      setLoading(false);
    }
  };

  const finishReservation = async (reservationId: string) => {
    try {
      console.log("Marking reservation as finished:", reservationId);
      const response = await instance.put("/Admin/finish-reservation", null, {
        params: { reservationId },
      });
      console.log("Reservation finished successfully:", response.data);
    } catch (error) {
      console.error("Error finishing reservation:", error);
      throw new Error("Failed to finish the reservation.");
    }
  };

  const handleFeedbackSubmit = async (reservationId: string) => {
    try {
      // Step 1: Mark the reservation as finished
      console.log(`Finishing reservation with ID: ${reservationId}`);
      await finishReservation(reservationId);
      console.log(`Reservation ${reservationId} finished successfully.`);

      // Step 2: Submit feedback
      const feedbackData = {
        comment: "No comment",
        rating: feedbacks[reservationId],
        reservationID: reservationId,
        userID: userId,
      };

      console.log("Submitting Feedback:", feedbackData);
      const response = await instance.post("/Feedback/add-feedback", feedbackData);
      console.log("Feedback submitted successfully:", response.data);
      alert("Thank you for your feedback!");

      // Refresh reservations
      fetchUserReservations();
    } catch (error) {
      console.error("Error submitting feedback:", error);
      alert("Failed to submit feedback.");
    }
  };

  const isReservationEligibleForFeedback = (reservationDate: string) => {
    const reservationEndDate = new Date(reservationDate);
    const currentDate = new Date();
    return currentDate.getTime() - reservationEndDate.getTime() > 24 * 60 * 60 * 1000; // Check if more than a day has passed
  };

  useEffect(() => {
    fetchUserReservations();
  }, []);

  return (
    <div>
      <h2>My Reservations</h2>
      {loading ? (
        <p>Loading reservations...</p>
      ) : reservations.length > 0 ? (
        <table border="1">
          <thead>
            <tr>
              <th>Restaurant</th>
              <th>Reservation ID</th>
              <th>Date</th>
              <th>Number of People</th>
              <th>Table Number</th>
              <th>Confirmed</th>
              <th>Feedback</th>
            </tr>
          </thead>
          <tbody>
            {reservations.map((reservation: any) => (
              <tr key={reservation.reservationId}>
                <td>{reservation.restaurantName}</td>
                <td>{reservation.reservationId}</td>
                <td>{reservation.reservationDate}</td>
                <td>{reservation.numberOfPeople}</td>
                <td>{reservation.tableNumber}</td>
                <td>{reservation.isConfirmed ? "Yes" : "No"}</td>
                <td>
                  {isReservationEligibleForFeedback(reservation.reservationDate) ? (
                    <div>
                      <label>Rate:</label>
                      <select
                        value={feedbacks[reservation.reservationId] || 0}
                        onChange={(e) =>
                          setFeedbacks({
                            ...feedbacks,
                            [reservation.reservationId]: parseInt(e.target.value),
                          })
                        }
                      >
                        <option value="0">Select</option>
                        <option value="1">1 Star</option>
                        <option value="2">2 Stars</option>
                        <option value="3">3 Stars</option>
                        <option value="4">4 Stars</option>
                        <option value="5">5 Stars</option>
                      </select>
                      <button
                        onClick={() => handleFeedbackSubmit(reservation.reservationId)}
                        disabled={!feedbacks[reservation.reservationId]}
                      >
                        Submit
                      </button>
                    </div>
                  ) : (
                    "N/A"
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>No reservations found.</p>
      )}
    </div>
  );
};

export default UserReservationsPage;
