using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("media")]
    public class Media : IdAndDatesWithIsDeleted
    {
        [Column("url")]
        public string Url { get; set; } = string.Empty;

        [Column("alt_text")]
        public string AltText { get; set; } = string.Empty;

        [Column("type")]
        public long Type { get; set; }

        [Column("type_name")]
        public string TypeName { get; set; } = string.Empty;
    }
}
