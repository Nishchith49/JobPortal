using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("role")]
    public class Role : PrimaryKey
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [InverseProperty(nameof(UserRole.Role))]
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
