// EraAgentDashboard.Domain/Entities/CallLog.cs
// *** REMOVE all [JsonPropertyName(...)] attributes and the using System.Text.Json.Serialization; ***
using System;
using System.Collections.Generic;
// NO using System.Text.Json.Serialization;

namespace EraAgentDashboard.Domain.Entities;

// --- Pure Domain Entity ---
public class CallLog
{
    public string? Id { get; set; }
    public string? OrgId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public double? Cost { get; set; }
    public string? PhoneCallProvider { get; set; }
    public string? PhoneCallTransport { get; set; }
    public string? EndedReason { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public CallLogDestination? Destination { get; set; }
    public CallLogCostBreakdown? CostBreakdown { get; set; }
    public CallLogArtifact? Artifact { get; set; }
    public CallLogCustomer? Customer { get; set; }
    public List<CallLogCostDetail>? CostDetails { get; set; }
}

// --- Pure Nested Domain Classes (NO attributes) ---
public class CallLogDestination
{
    public string? Type { get; set; }
    public string? Number { get; set; }
    public string? CallerId { get; set; }
    public string? Description { get; set; }
}

public class CallLogCostBreakdown { /* Properties without attributes */ }
public class CallLogArtifact { /* Properties without attributes */ }
public class CallLogCustomer
{
    public string? Number { get; set; }
    public string? Name { get; set; }
}
public class CallLogCostDetail { /* Properties without attributes */ }

