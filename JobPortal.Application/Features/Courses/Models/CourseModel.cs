using JobPortal.Application.Features.Courses.Commands;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Courses.Models
{
    public class CourseModel : UpdateCourseCommand
    {
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("lastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }
    }
}