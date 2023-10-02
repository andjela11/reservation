namespace Domain;

public class Reservation : BaseEntity
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int SeatNumbers { get; set; }
}
