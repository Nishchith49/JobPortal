using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Courses.Commands
{
    public class EnableOrDisableCourseCommand : IRequest<APIResponse>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
