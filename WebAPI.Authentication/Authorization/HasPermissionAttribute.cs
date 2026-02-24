using Microsoft.AspNetCore.Authorization;
using WebAPI.Authentication.Domain.Enum;

namespace WebAPI.Authentication.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public string Permission { get; }

        // Resource can be optional. If null, infer from controller
        public HasPermissionAttribute(PermissionAction action, string? resource = null)
        {
            Policy = "PermissionPolicy";
            Permission = $"{resource ?? "AUTO"}.{action}";
            Action = action;
        }

        public PermissionAction Action { get; }
    }
}