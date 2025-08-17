using EcoInspira.Domain.Repositories;
using EcoInspira.Domain.Repositories.User;
using EcoInspira.Infrastructure.DataAccess;
using EcoInspira.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcoInspira.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services) 
        {
            AddDbContext_MySqlServer(services);
            AddRepositories(services);
        }


        private static void AddDbContext_MySqlServer(IServiceCollection services)
        {
            var connectionString = "Server=localhost;DataBase=ecoinspira;Uid=root;Pwd=123123";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 37));

            services.AddDbContext<EcoInspiraDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql (connectionString, serverVersion);
            });

        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnityOfWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
