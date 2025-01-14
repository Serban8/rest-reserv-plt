import React, { useState } from "react";
import { Button, Grid } from "@mui/material";

const SeatSelector = ({ tables, onConfirm }) => {
  const [selectedTables, setSelectedTables] = useState([]);

  const toggleTable = (tableId) => {
    setSelectedTables((prev) =>
      prev.includes(tableId)
        ? prev.filter((id) => id !== tableId)
        : [...prev, tableId]
    );
  };

  return (
    <div>
      <h3>Select a Table</h3>
      <Grid container spacing={2}>
        {tables.map((table) => (
          <Grid item key={table.id}>
            <Button
              variant="contained"
              color={selectedTables.includes(table.id) ? "success" : "secondary"}
              onClick={() => toggleTable(table.id)}
            >
              {`Table ${table.tableNumber} (${table.seats} seats)`}
            </Button>
          </Grid>
        ))}
      </Grid>
      <button
        onClick={() => onConfirm(selectedTables)}
        disabled={selectedTables.length === 0}
      >
        Confirm Reservation
      </button>
    </div>
  );
};

export default SeatSelector;
