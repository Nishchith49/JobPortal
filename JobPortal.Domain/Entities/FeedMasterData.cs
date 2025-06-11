using Microsoft.EntityFrameworkCore;

namespace JobPortal.Domain.Entities
{
    public static class FeedMasterData
    {
        private static readonly Guid AdminUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private static readonly Guid AdminRoleId = Guid.Parse("00000000-0000-0000-0000-000000000002");
        private static readonly Guid AdminUserRoleId = Guid.Parse("00000000-0000-0000-0000-000000000003");

        public static void SeedMasterData(this ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedAdminUser(modelBuilder);
        }

        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = AdminRoleId,
                Name = "Admin"
            });
        }

        public static void SeedAdminUser(this ModelBuilder modelBuilder)
        {
            var date = new DateTime(2025, 06, 06);
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = AdminUserId,
                EmailId = "admin@admin.com",
                PhoneNumber = "9999999999",
                UserName = "Admin",
                Password = "p7D/ukHhRwG3KDJKcbfMlJqrZRNEeyxW1wKAFWbTHbI=",
                IsActive = true,
                CreatedDate = date,
                UpdatedDate = date
            });

            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id = AdminUserRoleId,
                UserId = AdminUserId,
                RoleId = AdminRoleId,
            });
        }
    }
}
