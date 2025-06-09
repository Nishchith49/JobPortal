using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Jobs.Commands
{
    public class DeleteJobCommand : IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public DeleteJobCommand(Guid id) => Id = id;
    }
}
