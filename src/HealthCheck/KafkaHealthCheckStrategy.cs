using AutoHealthStatus.Models;

namespace AutoHealthStatus.HealthCheck;

public class KafkaHealthCheckStrategy : HealthCheckStrategyBase
{

    const string SupportedPortal = "Kafka";
    const string TopicName = "topic"; //TODO: Change as needed

    public KafkaHealthCheckStrategy(PortalConfig portalConfig) : base(portalConfig) { }

    public override bool CanExecute()
    {
        return Portal?.Name == SupportedPortal;
    }

    public async override Task<bool> PerformHealthCheckAsync()
    {
        try
        {
            await _page.GetByPlaceholder("Search topics").ClickAsync();
            await _page.GetByPlaceholder("Search topics").FillAsync(TopicName);
            await _page.WaitForTimeoutAsync(200000); //TODO: Prefer waiting for selector instead of timeout
            await _page.WaitForSelectorAsync("div.Stack-sc-1vnifuo-0>div:nth-child(2)>span:text('--')");

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