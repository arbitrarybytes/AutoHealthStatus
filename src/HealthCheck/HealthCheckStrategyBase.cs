using AutoHealthStatus.Models;
using Microsoft.Playwright;

namespace AutoHealthStatus.HealthCheck;

public abstract class HealthCheckStrategyBase : IHealthCheckStrategy
{
    const string ScreenshotsFolder = "Screenshots";
    
    protected IBrowser _browser;
    protected IPage _page;

    protected PortalConfig Portal { get; }

    public HealthCheckStrategyBase(PortalConfig config)
    {
        Portal = config;
    }

    public virtual async Task<bool> ExecuteAsync(IBrowser browser)
    {
        _browser = browser;
        try
        {
            return await LoginAsync();
        }
        catch (Exception ex)
        {
            $"Exception while executing health check for {Portal.Name}.\nDetails:{ex.Message}".LogAsError();
            return false;
        }
    }

    public async Task<bool> LoginAsync()
    {
        try
        {
            var options = new BrowserNewPageOptions();

            if (Portal.Authentication == AuthenticationType.Basic)
                options.HttpCredentials = new HttpCredentials { Password = Portal.Password, Username = Portal.Username };

            _page = await _browser.NewPageAsync(options);

            await _page.GotoAsync(Portal.PortalUrl);

            if (Portal.Authentication == AuthenticationType.Forms)
            {
                await _page.WaitForSelectorAsync(Portal.LoginSelector);
                await _page.FillAsync(Portal.UsernameSelector, Portal.Username);
                await _page.FillAsync(Portal.PasswordSelector, Portal.Password);

                await _page.ClickAsync(Portal.LoginButtonSelector);
            }

            if (!string.IsNullOrWhiteSpace(Portal.PostLoginSelector))
                await _page.WaitForSelectorAsync(Portal.PostLoginSelector);

            return true;
        }
        catch (System.Exception ex)
        {
            $"Exception at Login for {Portal.Name} with {Portal.Authentication}\nDetails: {ex.Message}".LogAsError();
            return false;
        }
    }

    public abstract Task<bool> PerformHealthCheckAsync();

    public async Task<bool> TakeScreenShotAsync(string selector, PortalConfig config)
    {
        try
        {
            var path = $"{ScreenshotsFolder}/{config.Name}_{DateTime.Now:yyyy-MM-dd-hh-mm}.png";

            if (string.IsNullOrWhiteSpace(selector)) //full page
            {
                await _page.ScreenshotAsync(new PageScreenshotOptions
                {
                    FullPage = true,
                    Path = path
                });
            }
            else //selector only
            {
                await _page.Locator(selector).ScreenshotAsync(new LocatorScreenshotOptions { Path = path });
            }

            $"{Portal.Name} - Screenshot saved at {path}".LogAsSuccess();

            return true;
        }
        catch (Exception ex)
        {
            $"Exception while taking screenshot for {Portal.Name} with {Portal.Authentication}\nDetails: {ex.Message}".LogAsError();
            return false;
        }
    }

    public abstract bool CanExecute();
}