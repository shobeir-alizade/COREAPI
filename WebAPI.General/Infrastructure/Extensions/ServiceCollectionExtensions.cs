using WebAPI.Core.Extenstions;
using WebAPI.Core.Infrastructure;
using WebAPI.Core.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
namespace WebAPI.Infrastructure.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var ListTypes = typeof(IApplicationStartup).GetAllClassTypes();

            List<IApplicationStartup> ListObjects = new List<IApplicationStartup>();

            foreach (var Typeitem in ListTypes)
            {
                var ob = Activator.CreateInstance(Typeitem) as IApplicationStartup;
                ListObjects.Add(ob);
            }

            foreach (var item in ListObjects.OrderBy(p=>p.Priority))
            {
                item.ConfigureServices(services, configuration);
            }


            var ListTaskTypes = typeof(ITaskSchduler).GetAllClassTypes();
            foreach (var typeTask in ListTaskTypes)
            {
                services.AddScoped(typeTask);
            }
        }
    }

}
