using Newtonsoft.Json;

namespace JobPortal.Application.Features.DropDowns.Models
{
    public class DropDownModel 
    {
        [JsonProperty("label")]
        public string Label { get; set; } = string.Empty;

        [JsonProperty("value")]
        public Guid Value { get; set; }
    }
}
