import React from "react";
import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { Link } from "react-router-dom";

const Navbar: React.FC = () => {
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
              to="/"
              style={{ textDecoration: "none", color: "white", fontWeight: 600 }}
            >
              Home
            </Link>
          </Button>

          <Button color="inherit">
            <Link
              to="/restaurants"
              style={{ textDecoration: "none", color: "white", fontWeight: 600 }}
            >
              Restaurants
            </Link>
          </Button>

          <Button color="inherit">
            <Link
              to="/reservations"
              style={{ textDecoration: "none", color: "white", fontWeight: 600 }}
            >
              Reservations
            </Link>
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
