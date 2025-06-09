using MediatR;
using JobPortal.Application.Features.ClientCompanies.Models;
using Newtonsoft.Json;
using JobPortal.Infrastructure.Global;

namespace JobPortal.Application.Features.ClientCompanies.Queries
{
    public class GetClientCompanyByIdQuery : IRequest<ServiceResponse<ClientCompanyModel>>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public GetClientCompanyByIdQuery(Guid id) => Id = id;
    }
}