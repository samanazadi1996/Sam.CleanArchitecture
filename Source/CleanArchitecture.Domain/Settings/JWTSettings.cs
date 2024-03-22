namespace CleanArchitecture.Domain.Settings
{
    public class JWTSettings
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public double DurationInMinutes { get; set; }
    }
}