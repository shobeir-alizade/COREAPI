namespace WebAPI.Core.Domain.Auth.Identity
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Parentid { get; set; }

        public ICollection<RoleMenu> RoleMenus { get; set; } 
        public ICollection<PageMenu> PageMenus { get; set; } 
    }
}
