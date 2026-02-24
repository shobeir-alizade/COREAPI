using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using WebAPI.Authentication.Domain.Enum;

namespace WebAPI.Authentication.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var httpContext = context.Resource as HttpContext;
            var endpoint = httpContext?.GetEndpoint();

            var attr = endpoint?.Metadata.GetMetadata<HasPermissionAttribute>();
            if (attr == null)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // Get controller name if resource is AUTO
            var controllerName = (endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>()?.ControllerName) ?? "Unknown";

            var resource = attr.Permission.Split('.')[0] == "AUTO"
                ? controllerName
                : attr.Permission.Split('.')[0];

            var requiredPermission = $"{resource}.{attr.Action}";

            var userPermissions = context.User
                .FindAll("permission")
                .Select(c => c.Value);

            if (userPermissions.Contains(requiredPermission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}