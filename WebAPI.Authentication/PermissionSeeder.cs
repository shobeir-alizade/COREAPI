using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Authentication.Authorization;
using WebAPI.Authentication.Domain.Authorization;
using WebAPI.Authentication.Domain.Enum;
using WebAPI.Authentication.Infrastructure.Persistence;
 

namespace WebAPI.Authentication
{
    public static class PermissionSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            var controllers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(t));

            foreach (var controller in controllers)
            {
                var resource = controller.Name.Replace("Controller", "");

                var methods = controller.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(HasPermissionAttribute), false).Any());

                foreach (var method in methods)
                {
                    var attr = method.GetCustomAttribute<HasPermissionAttribute>();
                    var action = attr!.Action;

                    var exists = await db.Permissions.AnyAsync(p =>
                        p.Resource == resource &&
                        p.Action  == action);

                    if (!exists)
                    {
                        db.Permissions.Add(new Permission
                        {
                            Resource = resource,
                            Action = attr.Action
                        });
                    }
                }
            }

            await db.SaveChangesAsync();
        }
    }
}