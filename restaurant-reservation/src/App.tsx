import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar.tsx";
import HomePage from "./pages/HomePage.tsx";
import RestaurantPage from "./pages/RestaurantPage.tsx";
import ReservationPage from "./pages/ReservationPage.tsx";

const App: React.FC = () => {
  return (
    <Router>
      {/* Navbar */}
      <Navbar />

      {/* Main Content */}
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/restaurants" element={<RestaurantPage />} />
        <Route path="/reservations" element={<ReservationPage />} />
      </Routes>
    </Router>
  );
};

export default App;
