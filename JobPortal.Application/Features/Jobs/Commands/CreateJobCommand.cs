using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Jobs.Commands
{
    public class CreateJobCommand : IRequest<APIResponse>
    {
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        [JsonProperty("skillsRequired")]
        public string SkillsRequired { get; set; } = string.Empty;

        [JsonProperty("experienceRequired")]
        public int ExperienceRequired { get; set; }

        [JsonProperty("jobType")]
        public string JobType { get; set; } = string.Empty; // e.g., Full-time, Part-time, Contract

        [JsonProperty("salary")]
        public decimal Salary { get; set; }

        [JsonProperty("clientCompanyId")]
        public Guid ClientCompanyId { get; set; }
    }
}
