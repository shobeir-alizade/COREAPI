using WebAPI.Core.Infrastructure;
using WebAPI.Service.Catalog;
using WebAPI.Service.Media;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Service.Infrastructure
{
    public class CommonStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<MapsterConfigMiddleWare>();

        }

       

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IPictureService, PictureService>();

        }


    }
}
