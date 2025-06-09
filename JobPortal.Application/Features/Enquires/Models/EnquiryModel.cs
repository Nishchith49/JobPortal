using JobPortal.Application.Features.Enquires.Commands;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Enquires.Models
{
    public class EnquiryModel : CreateEnquiryCommand
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("isResolved")]
        public bool IsResolved { get; set; } = false;

        [JsonProperty("resolvedDate")]
        public DateTime? ResolvedDate { get; set; }
    }
}
