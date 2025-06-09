using JobPortal.Application.Features.ClientCompanies.Commands;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.ClientCompanies.Models
{
    public class ClientCompanyModel : UpdateClientCompanyCommand
    {
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("lastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }
    }
}