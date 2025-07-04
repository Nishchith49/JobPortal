using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Applicants.Commands
{
    public class UpdateApplicantCommand : CreateApplicantCommand, IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}