using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("job")]
    public class Job : IdAndDatesWithIsActiveAndIsDeleted
    {
        public Job()
        {
            Applicants = new HashSet<Applicant>();
        }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("location")]
        public string Location { get; set; } = string.Empty;

        [Column("skills_required")]
        public string SkillsRequired { get; set; } = string.Empty;

        [Column("experience_required")]
        public int ExperienceRequired { get; set; }

        [Column("job_type")]
        public string JobType { get; set; } = string.Empty; // e.g., Full-Time, Part-Time

        [Column("salary")]
        public decimal Salary { get; set; }

        [Column("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        [Column("education")]
        public string Education { get; set; } = string.Empty;

        [Column("client_company_id")]
        public Guid ClientCompanyId { get; set; }

        [ForeignKey(nameof(ClientCompanyId))]
        [InverseProperty(nameof(ClientCompany.Jobs))]
        public ClientCompany ClientCompany { get; set; } = null!;

        [InverseProperty(nameof(Applicant.Job))]
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}
