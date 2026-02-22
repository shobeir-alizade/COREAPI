using Microsoft.AspNetCore.Authorization;
using WebAPI.Authentication.Domain.Enum;

namespace WebAPI.Authentication.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string resource, PermissionAction action)
        {
            Policy = $"{resource}.{action}";
        }
    }
}
