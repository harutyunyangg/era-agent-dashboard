// EraAgentDashboard.Infrastructure/HttpClients/VapiClient.cs
using AutoMapper; // Add this using
using EraAgentDashboard.Application.Interfaces;
using EraAgentDashboard.Domain.Entities;
using EraAgentDashboard.Infrastructure.HttpClients.VapiDtos;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json; // Keep for potential JsonException

namespace EraAgentDashboard.Infrastructure.HttpClients;

public class VapiClient : IVapiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<VapiClient> _logger;
    private readonly IMapper _mapper; // Add IMapper field

    // Inject IMapper into the constructor
    public VapiClient(HttpClient httpClient, ILogger<VapiClient> logger, IMapper mapper)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); // Store injected mapper
    }

    public async Task<IEnumerable<Domain.Entities.CallLog>> GetCallLogsAsync(CancellationToken cancellationToken = default)
    {
        const string requestUri = "call";
        try
        {
            // 1. Deserialize into Infrastructure DTOs
            var vapiDtos = await _httpClient.GetFromJsonAsync<List<VapiCallLogDto>>(requestUri, cancellationToken);

            if (vapiDtos == null || !vapiDtos.Any()) // Check if list is null or empty
            {
                _logger.LogInformation("No call logs received from Vapi API at {RequestUri}", requestUri);
                return Enumerable.Empty<Domain.Entities.CallLog>(); // Return empty enumerable
            }

            // 2. Map Infrastructure DTOs to Domain Entities using AutoMapper
            var domainLogs = _mapper.Map<List<Domain.Entities.CallLog>>(vapiDtos);

            return domainLogs;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP Error fetching call logs from Vapi API at {RequestUri}", requestUri);
            return Enumerable.Empty<Domain.Entities.CallLog>(); // Return empty on error
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON Error parsing call logs response from Vapi API at {RequestUri}", requestUri);
            return Enumerable.Empty<Domain.Entities.CallLog>(); // Return empty on error
        }
        catch (AutoMapperMappingException ex) // Catch potential mapping errors
        {
             _logger.LogError(ex, "AutoMapper Error mapping call logs from Vapi DTOs for {RequestUri}", requestUri);
             // Depending on requirements, you might want to return empty or re-throw
             return Enumerable.Empty<Domain.Entities.CallLog>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching or mapping call logs from Vapi API at {RequestUri}", requestUri);
            throw; // Re-throw unexpected errors
        }
    }

    // --- Remove the manual mapping helper methods ---
    // private CallLogCostBreakdown? MapCostBreakdown(VapiCostBreakdownDto? dto) { ... }
    // private CallLogArtifact? MapArtifact(VapiArtifactDto? dto) { ... }
    // private CallLogCostDetail MapCostDetail(VapiCostDetailDto dto) { ... }
}
