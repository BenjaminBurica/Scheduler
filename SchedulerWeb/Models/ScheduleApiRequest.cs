namespace SchedulerWeb.Models;
using Entities;
public class ScheduleApiRequest
{
    public string? PhoneNumber { get; set; }
    public List<StoreSchedule> Schedule { get; set; } = [];
}