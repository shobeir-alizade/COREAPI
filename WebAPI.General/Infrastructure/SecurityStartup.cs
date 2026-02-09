using WebAPI.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Framework.Infrastructure
{
    public class SecurityStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.High;

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}
