using AutoHealthStatus.Models;

namespace AutoHealthStatus;
class Program
{
    const string ConfigFileName = "HealthCheckConfig.json";
    static HealthCheckConfig _config = null;

    static void Main(string[] args)
    {
        if(!LoadConfig()) return;

        ProcessConfigs();
    }

    private static void ProcessConfigs()
    {
        $"Processing config started".LogAsInfo();

        foreach(var config in _config.PortalConfigs)
        {
            $"[{config.Name} - {config.Authentication}] - Processing started".LogAsInfo();
        }

        $"Processing config started".LogAsSuccess();
    }

    static bool LoadConfig()
    {
        try
        {
            $"Loading health check config".LogAsInfo();
            _config = System.Text.Json.JsonSerializer.Deserialize<HealthCheckConfig>(System.IO.File.ReadAllText(ConfigFileName));
            $"Config loaded successfully with [{_config.PortalConfigs?.Count}] portals".LogAsSuccess();
            return true;
        }
        catch (System.Exception ex)
        {
            $"Error loading configuration, please check the below path:\n{System.IO.Path.GetFullPath(ConfigFileName)}\nDetails: {ex.Message}".LogAsError();
            return false;
        }
    }

}