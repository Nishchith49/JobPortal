using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    public class PrimaryKey
    {
        [Key, Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public class IdWithDateColumns : PrimaryKey
    {
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }

    public class IdWithCreatedDate : PrimaryKey
    {
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    public class IdAndDatesWithIsActive : IdWithDateColumns
    {
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
    }

    public class IdAndDatesWithIsDeleted : IdWithDateColumns
    {
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }

    public class IdAndDatesWithIsActiveAndIsDeleted : IdAndDatesWithIsActive
    {
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
