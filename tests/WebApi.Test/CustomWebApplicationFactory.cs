using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EcoInspira.Infrastructure.DataAccess;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EcoInspiraDbContext>));

                    if (descriptor is not null)
                        services.Remove(descriptor);

                    var provider = services.AddEntityFrameworkMySql ().BuildServiceProvider();

                    services.AddDbContext<EcoInspiraDbContext>(options =>
                    {
                        options.UseInternalServiceProvider(provider);
                    });
                });
        }
    }
}
