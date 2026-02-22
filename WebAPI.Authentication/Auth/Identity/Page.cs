namespace WebAPI.Authentication.Domain.Identity
{
    public class Page
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Route { get; set; } = null!;

        public ICollection<RolePage> RolePages { get; set; }  
        public ICollection<PageMenu> PageMenus { get; set; }  
    }

}
