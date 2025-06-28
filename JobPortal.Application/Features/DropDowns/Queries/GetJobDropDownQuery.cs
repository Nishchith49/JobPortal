using JobPortal.Application.Features.DropDowns.Models;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.DropDowns.Queries
{
    public class GetJobDropDownQuery : IRequest<List<DropDownModel>>
    {
        [JsonProperty("clientCompanyId")]
        public Guid? ClientCompanyId { get; set; }

        public GetJobDropDownQuery(Guid? clientCompanyId) => ClientCompanyId = clientCompanyId;
    }
}
