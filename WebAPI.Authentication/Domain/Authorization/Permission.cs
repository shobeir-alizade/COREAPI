using WebAPI.Authentication.Domain.Enum;

namespace WebAPI.Authentication.Domain.Authorization
{
    public class Permission
    {
        public int Id { get; set; }
        public string Resource { get; set; } = default!;
        public PermissionAction Action { get; set; }

        public string Code => $"{Resource}.{Action}";
    }
}
