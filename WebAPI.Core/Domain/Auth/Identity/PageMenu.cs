namespace WebAPI.Core.Domain.Auth.Identity
{
    public class PageMenu
    {
        public int PageId { get; set; }
        public Page Page { get; set; } = null!;

        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;
    }
}
