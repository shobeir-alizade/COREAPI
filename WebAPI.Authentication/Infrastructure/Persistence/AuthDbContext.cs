using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Authentication.Domain.Authorization;
using WebAPI.Authentication.Domain.Identity;

namespace WebAPI.Authentication.Infrastructure.Persistence
{
    public class ApplicationDbContext
      : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        //public DbSet<UserRole> UserRoles => Set<UserRole>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RolePermission>()
                .HasKey(x => new { x.RoleId, x.PermissionId });


            builder.Entity<RolePermission>().HasKey(x => new { x.RoleId, x.PermissionId });
            builder.Entity<RolePage>().HasKey(x => new { x.RoleId, x.PageId });
            builder.Entity<RoleMenu>().HasKey(x => new { x.RoleId, x.MenuId });
            builder.Entity<PageMenu>().HasKey(x => new { x.PageId, x.MenuId });

            //builder.Entity<UserRole>()
            //    .HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
