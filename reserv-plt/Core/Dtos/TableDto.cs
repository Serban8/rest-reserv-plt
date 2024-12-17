namespace Core.Dtos
{
    public class TableDto
    {
        public TableDto(Guid id, int tableNumber, bool isAvailable)
        {
            Id = id;
            TableNumber = tableNumber;
            IsAvailable = isAvailable;
        }

        public Guid Id { get; internal set; }

        public int TableNumber { get; internal set; }

        public bool IsAvailable { get; internal set; }
    }
}
