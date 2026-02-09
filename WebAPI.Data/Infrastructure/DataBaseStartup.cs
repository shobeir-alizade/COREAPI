 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repository;
using WebAPI.Data.Context;

namespace WebAPI.Data.Infrastructure
{
    public class DataBaseStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.AboveNormal;

        public void Configure(IApplicationBuilder app)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddDbContextPool<IApplcationDbContext, SqlServerApplicationContext>(
              c => c.UseSqlServer("Data Source=.;Initial Catalog=test002;Integrated Security=true;")
          , poolSize: 16);
        }
    }
}
