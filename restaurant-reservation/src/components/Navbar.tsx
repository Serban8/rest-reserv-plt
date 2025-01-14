import React from "react";
import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { Link } from "react-router-dom";

const Navbar: React.FC = () => {
  const token = localStorage.getItem("token");
  const name = localStorage.getItem("name"); // Retrieve user's name

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("name");
    window.location.reload();
  };

  return (
    <AppBar position="static" sx={{ backgroundColor: "#3f51b5" }}>
      <Toolbar>
        {/* Project/Logo Name */}
        <Typography
          variant="h6"
          component="div"
          sx={{ flexGrow: 1, fontWeight: "bold" }}
        >
          <Link to="/" style={{ textDecoration: "none", color: "white" }}>
            Restaurant Reservation
          </Link>
        </Typography>

        {/* Navigation Options */}
        <Box>
          <Button color="inherit">
            <Link
              to="/restaurants"
              style={{ textDecoration: "none", color: "white", fontWeight: 600 }}
            >
              Restaurants
            </Link>
          </Button>

          {token ? (
            <>
              <Button color="inherit">
                <Link
                  to="/reservations"
                  style={{
                    textDecoration: "none",
                    color: "white",
                    fontWeight: 600,
                  }}
                >
                  Reservations
                </Link>
              </Button>
              <Button color="inherit">
                <Link
                  to="/restaurant-reservations"
                  style={{
                    textDecoration: "none",
                    color: "white",
                    fontWeight: 600,
                  }}
                >
                  Restaurant Reservations
                </Link>
              </Button>
              <Typography
                variant="body1"
                sx={{ display: "inline", mx: 2, color: "white", fontWeight: 600 }}
              >
                Welcome, {name}
              </Typography>
              <Button
                color="inherit"
                onClick={handleLogout}
                style={{ fontWeight: 600 }}
              >
                Logout
              </Button>
            </>
          ) : (
            <>
              <Button color="inherit">
                <Link
                  to="/login"
                  style={{
                    textDecoration: "none",
                    color: "white",
                    fontWeight: 600,
                  }}
                >
                  Login
                </Link>
              </Button>
              <Button color="inherit">
                <Link
                  to="/register"
                  style={{
                    textDecoration: "none",
                    color: "white",
                    fontWeight: 600,
                  }}
                >
                  Register
                </Link>
              </Button>
            </>
          )}
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
