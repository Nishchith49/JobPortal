using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("applicant")]
    public class Applicant : IdAndDatesWithIsDeleted
    {
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("resume_url")]
        public string ResumeUrl { get; set; } = string.Empty;

        [Column("skills")]
        public string Skills { get; set; } = string.Empty;

        [Column("experience")]
        public int Experience { get; set; }

        [Column("job_id")]
        public Guid JobId { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("status_name")]
        public string StatusName { get; set; } = string.Empty;

        [ForeignKey(nameof(JobId))]
        [InverseProperty(nameof(Job.Applicants))]
        public Job Job { get; set; } = null!;
    }
}
