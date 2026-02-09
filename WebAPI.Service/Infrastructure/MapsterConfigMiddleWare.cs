using WebAPI.Service.Catalog;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace WebAPI.Service.Infrastructure
{
    public class MapsterConfigMiddleWare
    {
        private readonly RequestDelegate _next;
       
        public MapsterConfigMiddleWare(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);
            TypeAdapterConfig.GlobalSettings.Default.Ignore("Deleted");
            TypeAdapterConfig.GlobalSettings.Default.Ignore("TimeStamp");

            TypeAdapterConfig.GlobalSettings.Default.AddDestinationTransform((int? x) => x ?? 0);
            //TypeAdapterConfig.GlobalSettings.Default.EnableNonPublicMembers(true);
            await _next(context);
        }
    }

}
