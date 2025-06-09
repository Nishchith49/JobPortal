using MediatR;
using JobPortal.Application.Features.Applicants.Models;
using Newtonsoft.Json;
using JobPortal.Infrastructure.Global;

namespace JobPortal.Application.Features.Applicants.Queries
{
    public class GetApplicantByIdQuery : IRequest<ServiceResponse<ApplicantModel>>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public GetApplicantByIdQuery(Guid id) => Id = id;
    }
}