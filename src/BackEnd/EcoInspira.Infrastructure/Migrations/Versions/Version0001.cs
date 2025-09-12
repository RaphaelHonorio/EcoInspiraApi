using FluentMigrator;

namespace EcoInspira.Infrastructure.Migrations.Versions
{

    [Migration(1,"Create table to save the user's information")]
    public class Version0001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("User")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Password").AsString(2555).NotNullable()
                .WithColumn("Cpf").AsString(255).NotNullable()
                .WithColumn("DataNascimento").AsString(255).NotNullable()
                .WithColumn("UserIdentifier").AsGuid().NotNullable();
        }
    }
}
