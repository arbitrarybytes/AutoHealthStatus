using AutoHealthStatus.Models;
using Microsoft.Playwright;

namespace AutoHealthStatus.HealthCheck;

public class RabbitMQHealthCheckStrategy : HealthCheckStrategyBase
{

    const string SupportedPortal = "RabbitMQ";
    const string VirtualHost = "vhost"; //TODO: Change as needed

    public RabbitMQHealthCheckStrategy(PortalConfig portalConfig) : base(portalConfig) { }

    public override bool CanExecute()
    {
        return Portal?.Name == SupportedPortal;
    }

    public async override Task<bool> PerformHealthCheckAsync()
    {
        try
        {
            await _page.GetByRole(AriaRole.Combobox, new() { Name = "Virtual host" }).SelectOptionAsync(new[] { VirtualHost });
            await _page.WaitForSelectorAsync(".updatable");

            //Take page screenshot
            await TakeScreenShotAsync(string.Empty, Portal);

            return true;
        }
        catch (System.Exception ex)
        {
            $"Error performing health check for {Portal.Name}.\nDetails: {ex.Message}".LogAsError();
            return false;
        }
    }
}