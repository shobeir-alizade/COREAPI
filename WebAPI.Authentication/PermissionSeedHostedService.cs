using Microsoft.Extensions.Hosting;
using WebAPI.Authentication.Infrastructure.Persistence;
 

namespace WebAPI.Authentication
{
    public class PermissionSeedHostedService : IHostedService
    {
        private readonly IServiceProvider _services;

        public PermissionSeedHostedService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await PermissionSeeder.SeedAsync(db);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}