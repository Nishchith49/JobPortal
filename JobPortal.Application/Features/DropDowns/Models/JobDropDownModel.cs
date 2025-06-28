using Newtonsoft.Json;

namespace JobPortal.Application.Features.DropDowns.Models
{
    public class JobDropDownModel
    {
        [JsonProperty("clientCompanyName")]
        public string ClientCompanyName { get; set; } = string.Empty;

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; } = string.Empty;

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
