using AutoHealthStatus.Models;
using Microsoft.Playwright;

namespace AutoHealthStatus.HealthCheck;

internal interface IHealthCheckStrategy
{
    //TODO: Expose another virtual method to manipulate browser page initialization

    public Task<bool> LoginAsync(int? viewPortWidth = null, int? viewPortHeight = null);
    public Task<bool> PerformHealthCheckAsync();
    public Task<bool> TakeScreenShotAsync(string selector, PortalConfig config);
    public Task<bool> ExecuteAsync(IBrowser browser, int? preferredWidth = null, int? preferredHeight = null);

    public bool CanExecute();
}
