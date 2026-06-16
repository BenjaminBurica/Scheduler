namespace Entities;

public class StoreSchedule
{
    public int? Id { get; set; }
    public TimeOnly? Start { get; set; }
    public TimeOnly? End { get; set; }
    public bool? Closed { get; set; }
}
