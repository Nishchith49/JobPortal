using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Courses.Commands
{
    public class DeleteCourseCommand : IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public DeleteCourseCommand(Guid id) => Id = id;
    }
}