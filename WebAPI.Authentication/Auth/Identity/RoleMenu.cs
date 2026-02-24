namespace WebAPI.Authentication.Domain.Identity
{
    public class RoleMenu
    {
        public string RoleId { get; set; } = null!;
        public ApplicationRole Role { get; set; } = null!;

        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;
    }
}
