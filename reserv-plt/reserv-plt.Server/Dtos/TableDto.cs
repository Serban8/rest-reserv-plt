namespace reserv_plt.Server.Dtos
{
    public class TableDto
    (
         Guid Id,
         int TableNumber,
         bool IsAvailable
    )
    {
        public Guid Id { get; internal set; }

        public int TableNumber { get; internal set; }

        public bool IsAvailable { get; internal set; }
    }
}
