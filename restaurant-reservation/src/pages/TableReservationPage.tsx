import React, { useState } from "react";
import { useParams } from "react-router-dom";
import instance from "../api/axios";
import SeatSelector from "../components/SeatSelector.tsx";

const TableReservationPage: React.FC = () => {
  const { id } = useParams(); // Restaurant ID
  const [dateTime, setDateTime] = useState("");
  const [numberOfPeople, setNumberOfPeople] = useState(1);
  const [duration, setDuration] = useState("1h"); // Duration for reservation
  const [availableTables, setAvailableTables] = useState([]);

  // Fetch available tables based on restaurant ID and selected date/time
  const fetchAvailableTables = async () => {
    try {
      const response = await instance.get("/Table/all-tables", {
        params: {
          restaurantId: id,
          forDate: dateTime,
        },
      });

      setAvailableTables(response.data);
    } catch (error) {
      console.error("Error fetching available tables:", error);
    }
  };

  // Handle Search Button
  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    fetchAvailableTables();
  };
   
  // Handle reservation
  const handleReservation = async (selectedTableId: string) => {
    
    const reservationData = {
      reservationDate: dateTime,
      numberOfPeople: numberOfPeople,
      tableID: selectedTableId,
      userID: localStorage.getItem("userId"), // Assuming the user ID is stored in localStorage
    };

    console.log("Reservation Data:", reservationData); // Log the data being sent

    try {
      const response = await instance.post("/Table/reserve-table", reservationData);
      console.log("Reservation successful:", response.data);
      alert("Reservation successful!");
    } catch (error) {
      console.error("Error making reservation:", error);
      alert("Failed to make the reservation.");
    }
  };

  return (
    <div>
      <h2>Reserve a Table</h2>
      <form onSubmit={handleSearch}>
        <div>
          <label>Date and Time:</label>
          <input
            type="datetime-local"
            value={dateTime}
            onChange={(e) => setDateTime(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Number of People:</label>
          <input
            type="number"
            value={numberOfPeople}
            onChange={(e) => setNumberOfPeople(parseInt(e.target.value))}
            min="1"
            required
          />
        </div>
        <div>
          <label>Duration:</label>
          <select
            value={duration}
            onChange={(e) => setDuration(e.target.value)}
            required
          >
            <option value="1h">1 Hour</option>
            <option value="1.5h">1.5 Hours</option>
            <option value="2h">2 Hours</option>
          </select>
        </div>
        <button type="submit">Search Available Tables</button>
      </form>

      {availableTables.length > 0 && (
        <SeatSelector
          tables={availableTables.filter((table) => table.seats >= numberOfPeople)}
          onConfirm={(selectedTables) => {
            if (selectedTables.length > 0) {
              handleReservation(selectedTables[0]); // Reserve the first selected table
            } else {
              alert("Please select a table to reserve.");
            }
          }}
        />
      )}
    </div>
  );
};

export default TableReservationPage;
