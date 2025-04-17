using EraAgentDashboard.Components;
using EraAgentDashboard.Infrastructure;
using EraAgentDashboard.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- Clean Architecture Service Registration ---
builder.Services.AddApplicationServices(); // Register Application layer services (MediatR, etc.)
builder.Services.AddInfrastructureServices(builder.Configuration); // Register Infrastructure (HttpClient, VapiClient, Settings)
                                                                   // --- End Clean Architecture Service Registration ---

// Remove old CallLogService registration if it exists
// builder.Services.AddScoped<EraAgentDashboard.CallLogService>(); // REMOVE THIS
// Remove old HttpClient configuration if it exists outside Infrastructure DI

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
