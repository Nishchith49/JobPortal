using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Courses.Commands
{
    public class CreateCourseCommand : IRequest<APIResponse>
    {
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("durationInWeeks")]
        public int DurationInWeeks { get; set; }
    }
}