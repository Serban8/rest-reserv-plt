export const restaurants = [
    { id: 1, name: "The Fancy Spoon", description: "A high-end dining experience." },
    { id: 2, name: "Urban Deli", description: "Casual and modern eatery." },
    { id: 3, name: "Pasta Paradise", description: "Authentic Italian pasta dishes." },
  ];
  
  export const seatLayout = Array.from({ length: 20 }, (_, index) => ({
    id: index + 1,
    label: `Seat ${index + 1}`,
  }));
  