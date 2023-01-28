namespace AutoHealthStatus.Models;

public enum AuthenticationType
{
    None = 0, //Anonymous website, no auth required
    Basic = 1, //Basic Auth prompted by browser
    Forms = 2 //Forms based login on page
}