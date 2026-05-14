namespace Entities;

public class StoreSchedule
{
    public int Id { get; set; }
    public DateTime Day { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public bool Closed { get; set; }
}
