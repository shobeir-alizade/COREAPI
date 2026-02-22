using WebAPI.Authentication.Domain.Enum;

namespace WebAPI.Authentication.ViewModel
{
    public class CreatePermissionViewModel
    {
        public string Resource { get; set; } = default!;
        public PermissionAction Action { get; set; }
    }
}
