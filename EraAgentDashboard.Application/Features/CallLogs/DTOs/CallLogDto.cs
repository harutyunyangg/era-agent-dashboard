namespace EraAgentDashboard.Application.Features.CallLogs.DTOs;

// DTO to decouple UI from Domain Entity
public class CallLogDto
{
    public string? Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public double? Cost { get; set; }
    public string? DestinationNumber { get; set; }
    // Only include properties the UI actually needs
}
