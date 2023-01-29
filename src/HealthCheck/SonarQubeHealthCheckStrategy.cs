using AutoHealthStatus.Models;

namespace AutoHealthStatus.HealthCheck;

public class SonarQubeHealthCheckStrategy : HealthCheckStrategyBase
{

    const string SupportedPortal = "SonarQube";

    public SonarQubeHealthCheckStrategy(PortalConfig portalConfig) : base(portalConfig) { }

    public override bool CanExecute()
    {
        return Portal?.Name == SupportedPortal;
    }

    public async override Task<bool> PerformHealthCheckAsync()
    {
        try
        {
            await _page.ClickAsync("button>div.overview-measures-tab>span:text('Overall Code')");
            await _page.WaitForSelectorAsync(".overview-panel-content");

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
