namespace AutoHealthStatus.Models;

public class HealthCheckConfig
{
    public List<PortalConfig> PortalConfigs { get; set; }
    public ExportConfig ExportOptions { get; set; }
}
