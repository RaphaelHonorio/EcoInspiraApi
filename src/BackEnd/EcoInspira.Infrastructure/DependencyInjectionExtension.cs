using EcoInspira.Domain.Repositories;
using EcoInspira.Domain.Repositories.User;
using EcoInspira.Infrastructure.DataAccess;
using EcoInspira.Infrastructure.DataAccess.Repositories;
using EcoInspira.Infrastructure.Extensions;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EcoInspira.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration ) 
        {
            AddDbContext_MySqlServer(services, configuration);
            AddFluentMigrator_MySql(services, configuration);
            AddRepositories(services);
        }


        private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
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

        private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("EcoInspira.Infrastructure")).For.All();
            });
        }
    }
}
