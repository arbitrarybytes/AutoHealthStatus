using AutoHealthStatus.Models;
using Microsoft.Playwright;

namespace AutoHealthStatus.HealthCheck;

public class BlackDuckHealthCheckStrategy : HealthCheckStrategyBase
{

    const string SupportedPortal = "BlackDuck";
    const string AppName = "appname"; //TODO: Change as needed

    public BlackDuckHealthCheckStrategy(PortalConfig portalConfig) : base(portalConfig) { }

    public override bool CanExecute()
    {
        return Portal?.Name == SupportedPortal;
    }

    public async override Task<bool> PerformHealthCheckAsync()
    {
        try
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = AppName }).ClickAsync();
            await _page.WaitForSelectorAsync(".table-content-region");

            //Take page screenshot
            await TakeScreenShotAsync(string.Empty, Portal);

            return true;
        }
        catch (System.Exception ex)
        {
            Portal.LogError(ex.Message, nameof(PerformHealthCheckAsync));
            return false;
        }
    }
}