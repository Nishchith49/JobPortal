using JobPortal.Application.Features.Applicants.Commands;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Applicants.Models
{
    public class ApplicantModel : UpdateApplicantCommand
    {
        [JsonProperty("clientCompanyName")]
        public string ClientCompanyName { get; set; } = string.Empty;

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; } = string.Empty;

        [JsonProperty("appliedDate")]
        public DateTime AppliedDate { get; set; }

        [JsonProperty("lastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("statusName")]
        public string StatusName { get; set; } = string.Empty;
    }
}