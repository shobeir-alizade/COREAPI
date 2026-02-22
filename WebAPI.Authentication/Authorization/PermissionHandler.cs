using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authentication.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>

    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var endpoint = context.Resource as HttpContext;
            var permissionAttr = endpoint?
                .GetEndpoint()?
                .Metadata
                .GetMetadata<HasPermissionAttribute>();

            if (permissionAttr == null)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userPermissions = context.User
                .FindAll("permission")
                .Select(c => c.Value);

            if (userPermissions.Contains(permissionAttr.Permission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

