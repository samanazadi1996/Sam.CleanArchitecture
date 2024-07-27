namespace CleanArchitecture.Infrastructure.Identity.Settings;

#pragma warning disable
public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInMinutes { get; set; }
}
#pragma warning disable 
