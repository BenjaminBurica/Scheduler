namespace Entities;

public class CustomerProfileStep
{
    public int Id { get; set; }

    public int CustomerProfileId { get; set; }

    public int StepOrder { get; set; }

    public string StepType { get; set; } = "";

    public int DurationMinutes { get; set; }
}