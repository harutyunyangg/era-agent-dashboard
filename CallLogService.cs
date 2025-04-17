using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EraAgentDashboard;

public class CallLogService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "4de7999a-0b18-4c32-857c-d7408cd2c679";

    public CallLogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CallLog>> GetCallLogs()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        try
        {
            var response = await _httpClient.GetAsync("https://api.vapi.ai/call");
            response.EnsureSuccessStatusCode(); 

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new DateTimeConverter() }
            };

            var apiResponse = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json, options) ?? [];

            var callLogs = apiResponse.Select(item => new CallLog
            {
                Id = item.TryGetValue("id", out var id) ? id?.ToString() : null,
                OrgId = item.TryGetValue("orgId", out var orgId) ? orgId?.ToString() : null,
                CreatedAt = item.TryGetValue("createdAt", out var createdAt) && createdAt != null ? DateTime.Parse(createdAt.ToString()!) : null,
                UpdatedAt = item.TryGetValue("updatedAt", out var updatedAt) && updatedAt != null ? DateTime.Parse(updatedAt.ToString()!) : null,
                Type = item.TryGetValue("type", out var type) ? type?.ToString() : null,
                Status = item.TryGetValue("status", out var status) ? status?.ToString() : null,
                Cost = item.TryGetValue("cost", out var cost) && cost != null ?
          cost is System.Text.Json.JsonElement jsonElement && jsonElement.ValueKind == System.Text.Json.JsonValueKind.Number ?
            jsonElement.GetDouble() : null
           : null

            });

            return callLogs.ToList() ?? [];
        }
        catch (HttpRequestException ex)
        {
            // Handle network errors or API errors
            Console.WriteLine($"Error fetching call logs: {ex.Message}");
            return new List<CallLog>(); // Return an empty list or throw a custom exception
        }
        catch (JsonException ex)
        {
            // Handle JSON parsing errors
            Console.WriteLine($"Error parsing call logs: {ex.Message}");
            return new List<CallLog>();
        }
    }
}

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return default; // Or throw an exception, depending on your requirements
        }

        return DateTime.Parse(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("O")); // Use ISO 8601 format
    }
}