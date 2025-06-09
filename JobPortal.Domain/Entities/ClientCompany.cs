using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("client_company")]
    public class ClientCompany : IdAndDatesWithIsActiveAndIsDeleted
    {
        public ClientCompany()
        {
            Jobs = new HashSet<Job>();
        }

        [Column("company_name")]
        public string CompanyName { get; set; } = string.Empty;

        [Column("contact_person_name")]
        public string ContactPersonName { get; set; } = string.Empty;

        [Column("contact_email")]
        public string ContactEmail { get; set; } = string.Empty;

        [Column("contact_phone")]
        public string ContactPhone { get; set; } = string.Empty;

        [Column("designation")]
        public string Designation { get; set; } = string.Empty;

        [Column("company_website")]
        public string? CompanyWebsite { get; set; }

        [Column("linkedin_profile")]
        public string? LinkedInProfile { get; set; }

        [Column("location")]
        public string Location { get; set; } = string.Empty;

        [Column("notes")]
        public string? Notes { get; set; }

        [InverseProperty(nameof(Job.ClientCompany))]
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
