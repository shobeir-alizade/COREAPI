using WebAPI.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace WebAPI.Framework.Infrastructure
{
    public class CommonStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;

        public void Configure(IApplicationBuilder app)
        {
            IWebHostEnvironment env= app.ApplicationServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(app =>
                {
                    app.UseMiddleware<ErrorHandlerMiddleware>();
                });
                app.UseHsts();

            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop2 API V1");
            });
            app.UseMiddleware<PoweredByMiddleware>();


          

        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<IErrorHandler, ErrorHandler>();

            services.AddEndpointsApiExplorer();
            

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1.0", new OpenApiInfo
            //    {
            //        Version = "ورژن یک",
            //        Title = " وب سرویس فروشگاه",
            //        Description = " در این وب سرویس مشخصات فروشگاه شرح داده می شود",
            //        TermsOfService = new Uri("https://example.com/terms"),
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Devsharp Team",
            //            Email = string.Empty,
            //            Url = new Uri("https://twitter.com/devsharp"),
            //        },
            //        License = new OpenApiLicense
            //        {
            //            Name = "Use under LICX",
            //            Url = new Uri("https://devsharp.ir/license"),
            //        }
            //    });
            //});

        }
    }
}
