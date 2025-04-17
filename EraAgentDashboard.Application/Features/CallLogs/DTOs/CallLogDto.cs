// EraAgentDashboard.Application/Features/CallLogs/DTOs/CallLogDto.cs
namespace EraAgentDashboard.Application.Features.CallLogs.DTOs;

public class CallLogDto
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public decimal? Cost { get; set; }
    public string? DestinationNumber { get; set; } // From CallLog.Destination.Number
    public string? CustomerNumber { get; set; } // <-- Add this property

    // Add other properties needed by the UI
}
