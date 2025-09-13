using EcoInspira.Domain.Repositories;
using EcoInspira.Domain.Repositories.User;
using EcoInspira.Domain.Security.Tokens;
using EcoInspira.Infrastructure.DataAccess;
using EcoInspira.Infrastructure.DataAccess.Repositories;
using EcoInspira.Infrastructure.Extensions;
using EcoInspira.Infrastructure.Security.Tokens.Access.Generator;
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
            AddTokens(services, configuration);
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

        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        }
    }
}