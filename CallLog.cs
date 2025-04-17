namespace EraAgentDashboard;

public class CallLog
{
    public string? Id { get; set; }
    public string? OrgId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public double? Cost { get; set; }
    public string? DestinationNumber { get; set; }
    public string? PhoneCallProvider { get; set; }
}