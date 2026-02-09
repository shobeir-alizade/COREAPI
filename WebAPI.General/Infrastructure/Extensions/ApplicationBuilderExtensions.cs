using WebAPI.Core.Extenstions;
using Microsoft.AspNetCore.Builder;
using WebAPI.Core.Infrastructure;
using WebAPI.Core.Tasks;

namespace WebAPI.Infrastructure.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            var ListTypes = typeof(IApplicationStartup).GetAllClassTypes();


            List<IApplicationStartup> ListObjects = new List<IApplicationStartup>();

            foreach (var Typeitem in ListTypes)
            {
                var ob = Activator.CreateInstance(Typeitem) as IApplicationStartup;
                ListObjects.Add(ob);
            }

            foreach (var item in ListObjects.OrderBy(p => p.Priority))
            {
                item.Configure(application);
            }

            var ListTaskTypes = typeof(ITaskSchduler).GetAllClassTypes();
            foreach (var typeTask in ListTaskTypes)
            {
                var task = application.ApplicationServices.GetService(typeTask) as ITaskSchduler;
                if (task.IsActiveInStartup)
                    task.ExecuteTask();

            }

        }
    }

}
