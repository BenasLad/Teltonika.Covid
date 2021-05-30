namespace Teltonika.Covid.Api.Options
{
    public class JWTSettings
    {
        public const string Name = "JwtSettings";
        public string Secret { get; set; }
        public string ExpirationInMinutes { get; set; }
    }
}
