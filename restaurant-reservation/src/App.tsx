import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar.tsx";
import HomePage from "./pages/HomePage";
import RestaurantPage from "./pages/RestaurantPage.tsx";
import ReservationPage from "./pages/ReservationPage.tsx";
import RestaurantDetails from "./components/RestaurantDetails.tsx";
import RestaurantList from "./components/RestaurantList.tsx";
import LoginPage from "./pages/LoginPage.tsx"; // Import LoginPage
import RegisterPage from "./pages/RegisterPage.tsx"; // Import RegisterPage
import TableReservationPage from "./pages/TableReservationPage.tsx"; // Import TableReservationPage
import RestaurantReservationsPage from "./pages/RestaurantReservationPage.tsx"; // Import RestaurantReservationsPage

const App: React.FC = () => {
  return (
    <Router>
      {/* Navbar */}
      <Navbar />

      {/* Main Content */}
      <Routes>
        <Route path="/" element={<RestaurantList />} />
        <Route path="/restaurants" element={<RestaurantList />} />
        <Route path="/reservations" element={<ReservationPage />} />
        <Route path="/restaurant/:id" element={<RestaurantPage />} />
        <Route path="/restaurant/:id/reserve" element={<TableReservationPage />} />
        <Route path="/restaurant-reservations" element={<RestaurantReservationsPage />} />

        {/* Authentication Routes */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Routes>
    </Router>
  );
};

export default App;
