using JobPortal.Application.Features.Courses.Models;
using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Courses.Queries
{
    public class GetCourseByIdQuery : IRequest<ServiceResponse<CourseModel>>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public GetCourseByIdQuery(Guid id) => Id = id;
    }
}