namespace AutoHealthStatus.Models;

public class PortalConfig
{
    /// <summary>Gets or sets the Name of the capture target</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the Portal URL of the capture target</summary>
    public string PortalUrl { get; set; }

    /// <summary>Gets or sets the Username required for login</summary>
    public string Username { get; set; }

    /// <summary>Gets or sets the Password required for login</summary>
    public string Password { get; set; }

    /// <summary>The authentication type for the portal</summary>
    public AuthenticationType Authentication { get; set; }

    /// <summary>The selector to wait for to be loaded when Forms authentication is used</summary>
    public string LoginSelector { get; set; }

    /// <summary></summary>
    public string UsernameSelector { get; set; }

    /// <summary></summary>
    public string PasswordSelector { get; set; }
    
    /// <summary></summary>
    public string LoginButtonSelector { get; set; }
    
    /// <summary></summary>
    public string PostLoginSelector { get; set; }

    public string ExecutionStrategy { get; set; }

}
