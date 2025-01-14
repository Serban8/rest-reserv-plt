// mockData.ts

// Factory for creating restaurants
export const createRestaurant = (id: number, name: string, description: string) => ({
  id,
  name,
  description,
});

export const restaurants = [
  createRestaurant(1, "Gourmet Kitchen", "Fine dining experience"),
  createRestaurant(2, "Sushi World", "Authentic Japanese sushi"),
  createRestaurant(3, "Pasta Palace", "Italian cuisine"),
  createRestaurant(4, "Burger Haven", "Best burgers in town"),
  createRestaurant(5, "Vegan Delight", "Healthy plant-based meals"),
];

// Factory for creating seat layouts
export const createSeat = (id: number, label: string) => ({ id, label });

export const seatLayout = [
  createSeat(1, "A1"),
  createSeat(2, "A2"),
  createSeat(3, "A3"),
  createSeat(4, "B1"),
  createSeat(5, "B2"),
  createSeat(6, "B3"),
  createSeat(7, "C1"),
  createSeat(8, "C2"),
  createSeat(9, "C3"),
];
