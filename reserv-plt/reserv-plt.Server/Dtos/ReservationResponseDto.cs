namespace reserv_plt.Server.Dtos
{
    public class ReservationResponseDto
    (
        Guid reservationId,
        Guid TableId,
        string CustomerName,
        DateTime ReservationDate,
        bool IsConfirmed
    );
}
