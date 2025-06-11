using MediatR;
using JobPortal.Application.Features.Jobs.Models;
using JobPortal.Infrastructure.Global;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Jobs.Queries
{
    public class GetAllJobsQuery : PagedResponseInput, IRequest<PagedResponse<List<JobModel>>>
    {
        [JsonProperty("skills")]
        public string Skills { get; set; } = string.Empty;

        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        [JsonProperty("jobType")]
        public string JobType { get; set; } = string.Empty;
    }
}
