using DataLayer.Models;

namespace Core.Dtos
{
    public class TableDto
    {
        public TableDto(Guid id, int tableNumber, int seats, string position, Guid restaurantId, bool isAvailable)
        {
            Id = id;
            TableNumber = tableNumber;
            Seats = seats;
            Position = position;
            IsAvailable = isAvailable;
            RestaurantId = restaurantId;
        }

        public Guid Id { get; internal set; }

        public int TableNumber { get; internal set; }
        public int Seats { get; internal set; }
        public string Position { get; internal set; }

        public bool IsAvailable { get; internal set; }

        public Guid RestaurantId { get; internal set; }

        public static TableDto FromTable(Table table, bool isAvailable)
        {
            return new TableDto(table.Id, table.TableNumber, table.Seats, table.Position, table.RestaurantID, isAvailable);
        }
    }
}
