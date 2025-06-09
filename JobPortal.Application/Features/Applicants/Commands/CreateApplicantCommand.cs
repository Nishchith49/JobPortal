using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Applicants.Commands
{
    public class CreateApplicantCommand : IRequest<APIResponse>
    {
        [JsonProperty("fullName")]
        public string FullName { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonProperty("resumeUrl")]
        public string ResumeUrl { get; set; } = string.Empty;

        [JsonProperty("skills")]
        public string Skills { get; set; } = string.Empty;

        [JsonProperty("experience")]
        public int Experience { get; set; }

        [JsonProperty("jobId")]
        public Guid JobId { get; set; }
    }
}