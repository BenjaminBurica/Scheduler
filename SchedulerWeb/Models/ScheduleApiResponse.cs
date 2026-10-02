namespace SchedulerWeb.Models;
using Entities;
public class ScheduleApiResponse
{
    public string? PhoneNumber { get; set; }
    public List<StoreSchedule> Schedule { get; set; } = [];
}