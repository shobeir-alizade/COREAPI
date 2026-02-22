namespace WebAPI.Authentication.Domain.Identity
{
    public class RolePage
    {
        public string RoleId { get; set; } = null!;
        public ApplicationRole Role { get; set; } = null!;

        public int PageId { get; set; }
        public Page Page { get; set; } = null!;
    }
}
