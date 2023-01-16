namespace SchoolRegister.Models.Models;

/// <summary>
/// Class that represents Authentication Values from appsettings.json
/// </summary>
public class AuthenticationSettings
{
    public string JwtKey { get; set; }
    public int JwtExpireDays { get; set; }
    public string JwtIssuer { get; set; }
}