using EcoInspira.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace EcoInspira.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static DataBaseType DatabaseType(this IConfiguration configuration)
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");

            return (DataBaseType)Enum.Parse(typeof(DataBaseType), databaseType!);
        }

        public static string ConnectionString(this IConfiguration configuration)
        {
            var databaseType = configuration.DatabaseType();

            if (databaseType == DataBaseType.MySql)
                return configuration.GetConnectionString("Connection")!;
            else
                return configuration.GetConnectionString("OutroBancoDeDados")!;
        }
    }
}
