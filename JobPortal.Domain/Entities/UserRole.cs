using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("user_role")]
    public class UserRole : PrimaryKey
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.UserRoles))]
        public User User { get; set; } = null!;

        [Column("role_id")]
        public Guid RoleId { get; set; } 

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(Role.UserRoles))]
        public Role Role { get; set; } = null!;
    }
}
