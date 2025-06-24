using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Jobs.Commands
{
    public class EnableOrDisableJobCommand : IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
