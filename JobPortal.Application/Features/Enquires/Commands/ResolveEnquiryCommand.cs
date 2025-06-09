using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Enquires.Commands
{
    public class ResolveEnquiryCommand : IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public ResolveEnquiryCommand(Guid id) => Id = id;
    }
}
