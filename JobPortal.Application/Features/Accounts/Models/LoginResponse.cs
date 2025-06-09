using Newtonsoft.Json;

namespace JobPortal.Application.Features.Accounts.Models
{
    public class LoginResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonProperty("roles")]
        public List<string> Roles { get; set; } = new List<string>();

        [JsonProperty("tokenType")]
        public string TokenType { get; set; } = string.Empty;

        [JsonProperty("userName")]
        public string Username { get; set; } = string.Empty;
    }
}
