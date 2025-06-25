using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("course")]
    public class Course : IdAndDatesWithIsDeleted
    {
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("duration_in_weeks")]
        public int DurationInWeeks { get; set; } 
    }
}
