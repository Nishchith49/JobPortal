using JobPortal.Application.Features.Applicants.Commands;
using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.AJobPortal.Application.Features.Applicants.Commands
{
    public class UpdateApplicantCommand : CreateApplicantCommand, IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}