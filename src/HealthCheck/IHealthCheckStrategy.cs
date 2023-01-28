using AutoHealthStatus.Models;
using Microsoft.Playwright;

namespace AutoHealthStatus.HealthCheck;

internal interface IHealthCheckStrategy
{
    public Task<bool> LoginAsync();
    public Task<bool> PerformHealthCheckAsync();
    public Task<bool> TakeScreenShotAsync(string selector, PortalConfig config);

    public Task<bool> ExecuteAsync(IBrowser browser);

    public bool CanExecute();
}
