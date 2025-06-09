using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.ClientCompanies.Commands
{
    public class DeleteClientCompanyCommand : IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public DeleteClientCompanyCommand(Guid id) => Id = id;
    }
}