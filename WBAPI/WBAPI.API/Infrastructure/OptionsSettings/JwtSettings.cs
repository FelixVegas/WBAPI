namespace WBAPI.API.Infraestructure.OptionsSettings
{
    public class JwtSettings
    {
        public string SecretKey { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public int ExpiryDurationInMinutes { get; init; }
    }
}
