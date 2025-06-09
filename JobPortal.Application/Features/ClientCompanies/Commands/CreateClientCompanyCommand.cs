using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.ClientCompanies.Commands
{
    public class CreateClientCompanyCommand : IRequest<APIResponse>
    {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonProperty("contactPersonName")]
        public string ContactPersonName { get; set; } = string.Empty;

        [JsonProperty("contactEmail")]
        public string ContactEmail { get; set; } = string.Empty;

        [JsonProperty("contactPhone")]
        public string ContactPhone { get; set; } = string.Empty;

        [JsonProperty("designation")]
        public string Designation { get; set; } = string.Empty;

        [JsonProperty("companyWebsite")]
        public string? CompanyWebsite { get; set; }

        [JsonProperty("linkedInProfile")]
        public string? LinkedInProfile { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        [JsonProperty("notes")]
        public string? Notes { get; set; }
    }
}