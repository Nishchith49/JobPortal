using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Domain.Entities
{
    [Table("refresh_token")]
    public class RefreshToken : IdWithCreatedDate
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.RefreshTokens))]
        public User User { get; set; } = null!;

        [Column("token")]
        public string Token { get; set; } = string.Empty;

        [Column("expires")]
        public DateTime Expires { get; set; }
    }
}
