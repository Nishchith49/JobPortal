using JobPortal.Application.Features.Applicants.Models;
using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Applicants.Queries
{
    public class GetAllApplicantsQuery : PagedResponseInput, IRequest<PagedResponse<List<ApplicantModel>>>
    {
        [JsonProperty("clientCompanyId")]
        public Guid? ClientCompanyId { get; set; }

        [JsonProperty("jobId")]
        public Guid? JobId { get; set; }
    }
}