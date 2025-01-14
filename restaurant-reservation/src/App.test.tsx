// App.test.tsx with expanded tests

import { render, screen, fireEvent } from "@testing-library/react";
import App from "./App";

test("renders Navbar links", () => {
  render(<App />);
  expect(screen.getByText(/home/i)).toBeInTheDocument();
  expect(screen.getByText(/restaurants/i)).toBeInTheDocument();
  expect(screen.getByText(/reservations/i)).toBeInTheDocument();
});

test("displays restaurants on HomePage", () => {
  render(<App />);
  expect(screen.getByText(/gourmet kitchen/i)).toBeInTheDocument();
  expect(screen.getByText(/sushi world/i)).toBeInTheDocument();
});

test("navigates to restaurant details", () => {
  render(<App />);
  fireEvent.click(screen.getByText(/view details/i));
  expect(screen.getByText(/reserve a table/i)).toBeInTheDocument();
});

test("allows seat selection in SeatSelector", () => {
  render(<App />);
  fireEvent.click(screen.getByText(/view details/i));
  fireEvent.click(screen.getByText(/reserve a table/i));

  // Simulate seat selection
  fireEvent.click(screen.getByText(/a1/i));
  fireEvent.click(screen.getByText(/b2/i));
  expect(screen.getByText(/confirm reservation/i)).toBeInTheDocument();

  // Confirm selection
  fireEvent.click(screen.getByText(/confirm reservation/i));
});
