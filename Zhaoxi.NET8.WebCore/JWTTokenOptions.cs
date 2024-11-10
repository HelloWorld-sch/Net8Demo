namespace Zhaoxi.NET8.WebCore
{
    public class JWTTokenOptions
    {
        public string? Audience { get; set; }

        public string? SecurityKey { get; set; }

        public string? Issuer { get; set; }
    }
}
