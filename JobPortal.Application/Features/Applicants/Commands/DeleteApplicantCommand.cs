using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.AJobPortal.Application.Features.Applicants.Commands
{
    public class DeleteApplicantCommand : IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public DeleteApplicantCommand(Guid id) => Id = id;
    }
}