// EraAgentDashboard.Infrastructure/HttpClients/VapiDtos/VapiCallLogDto.cs
using System.Text.Json.Serialization; // Attributes are OK HERE

namespace EraAgentDashboard.Infrastructure.HttpClients.VapiDtos;

// DTO mirroring Vapi API JSON structure - LIVES IN INFRASTRUCTURE
public class VapiCallLogDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("orgId")]
    public string? OrgId { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("cost")]
    public double? Cost { get; set; }

    [JsonPropertyName("phoneCallProvider")]
    public string? PhoneCallProvider { get; set; }

    [JsonPropertyName("phoneCallTransport")]
    public string? PhoneCallTransport { get; set; }

    [JsonPropertyName("endedReason")]
    public string? EndedReason { get; set; }

    [JsonPropertyName("startedAt")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("endedAt")]
    public DateTime? EndedAt { get; set; }

    [JsonPropertyName("destination")]
    public VapiDestinationDto? Destination { get; set; } // Use nested Infra DTO

    [JsonPropertyName("costBreakdown")]
    public VapiCostBreakdownDto? CostBreakdown { get; set; } // Use nested Infra DTO

    [JsonPropertyName("artifact")]
    public VapiArtifactDto? Artifact { get; set; } // Use nested Infra DTO

    [JsonPropertyName("customer")]
    public VapiCustomerDto? Customer { get; set; } // Use nested Infra DTO

    [JsonPropertyName("costs")]
    public List<VapiCostDetailDto>? Costs { get; set; } // Use nested Infra DTO
    // Add all other fields from JSON with [JsonPropertyName] as needed
}

// --- Nested Infrastructure DTOs (with attributes) ---

public class VapiDestinationDto
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }

    [JsonPropertyName("callerId")]
    public string? CallerId { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

public class VapiCustomerDto
{
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

// Define VapiCostBreakdownDto, VapiArtifactDto, VapiCostDetailDto similarly with attributes
public class VapiCostBreakdownDto { /* Properties with attributes */ }
public class VapiArtifactDto { /* Properties with attributes */ }
public class VapiCostDetailDto { /* Properties with attributes */ }
