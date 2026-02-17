using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authentication.Authorization
{
    public class PermissionHandler
      : AuthorizationHandler<IAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,IAuthorizationRequirement requirement)
        {
            // Policy name == required permission
            var permission = context.Resource as string;

            if (permission != null &&
                context.User.HasClaim("permission", permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
