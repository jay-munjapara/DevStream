namespace DevStream.API.Models;

public class Deployment
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = "";
    public string Version { get; set; } = "";
    public string Environment { get; set; } = "dev"; // dev/stage/prod
    public string Status { get; set; } = "QUEUED";   // QUEUED/RUNNING/SUCCESS/FAILED
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}