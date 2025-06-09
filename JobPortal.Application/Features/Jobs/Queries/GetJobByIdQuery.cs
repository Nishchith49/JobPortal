using MediatR;
using JobPortal.Application.Features.Jobs.Models;
using Newtonsoft.Json;
using JobPortal.Infrastructure.Global;

namespace JobPortal.Application.Features.Jobs.Queries
{
    public class GetJobByIdQuery : IRequest<ServiceResponse<JobModel>>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public GetJobByIdQuery(Guid id) => Id = id;
    }
}
