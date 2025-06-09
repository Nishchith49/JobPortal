using JobPortal.Application.Features.Jobs.Commands;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Jobs.Models
{
    public class JobModel : UpdateJobCommand
    {
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("postedDate")]
        public DateTime PostedDate { get; set; }

        [JsonProperty("lastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }
    }
}