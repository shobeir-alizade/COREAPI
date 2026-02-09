using WebAPI.Core.Infrastructure;
using WebAPI.Framwork.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Framework.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
namespace WebAPI.Framework.Infrastructure
{
    public class ControllerStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Low;

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<LogFilterAttribute>();

            IMvcBuilder configController = services.AddControllers(
                option =>
                {

                    var policy = new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .Build();
                    option.Filters.Add(new AuthorizeFilter(policy));


                    option.Filters.Add(typeof(LogFilterAttribute));
                    option.ModelBinderProviders.Insert(0, new PersianDateEntityBinderProvider());
                }

                );


            //services.AddAuthentication("Bearer")
            // .AddJwtBearer("Bearer", options =>
            // {
            //     options.Authority = "https://localhost:6001";
            //     options.RequireHttpsMetadata = false;

            //     options.Audience = "ShopApi";

            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         NameClaimType = "name"

            //     };

            // });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
                                {
                                    options.Authority = "https://localhost:6001";
                                    options.RequireHttpsMetadata = false;

                                    options.Audience = "ShopApi";

                                    options.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        NameClaimType = "name"
                                    };
                                });



            configController.ConfigureApiBehaviorOptions((option) =>
            {

                option.ClientErrorMapping[404].Title = " منبع مورد نظر پیدا نشد ";

                option.InvalidModelStateResponseFactory = (Context) =>
                {

                    var values = Context.ModelState.Values.Where(state => state.Errors.Count != 0)
                  .Select(state => state.Errors.Select(p => new { errorMessage = p.ErrorMessage }));

                    return new BadRequestObjectResult(values);

                };
            });
        }
    }
}
