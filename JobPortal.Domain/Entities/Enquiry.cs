using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("enquiry")]
    public class Enquiry : IdWithCreatedDate
    {
        [Column("title")]
        public string Name { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("subject")]
        public string Subject { get; set; } = string.Empty;

        [Column("message")]
        public string Message { get; set; } = string.Empty; 

        [Column("is_resolved")]
        public bool IsResolved { get; set; } = false;

        [Column("resolved_date")]
        public DateTime? ResolvedDate { get; set; }
    }
}
