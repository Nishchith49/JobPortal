using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.ClientCompanies.Commands
{
    public class EnableOrDisableClientCompanyCommand : IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
