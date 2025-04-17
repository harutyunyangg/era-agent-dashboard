namespace EraAgentDashboard.Infrastructure.Settings;
public class VapiSettings
{
    public const string SectionName = "VapiApi"; // Matches appsettings.json section
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}
