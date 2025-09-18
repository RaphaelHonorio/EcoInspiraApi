using FluentMigrator;

namespace EcoInspira.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_POST, "Create table to save the posts's information")]
    public class Version0002 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Post")
                .WithColumn("Title").AsString(255).NotNullable()
                .WithColumn("Description").AsString(255).Nullable()
                .WithColumn("LikesCount").AsInt64().NotNullable()
                .WithColumn("CommentsCount").AsInt64().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Post_User_Id", "User", "Id");

            CreateTable("Comment")
                .WithColumn("Content").AsString(2000).NotNullable()
                .WithColumn("PostId").AsInt64().NotNullable().ForeignKey("FK_Comment_Post_Id", "Post", "Id")
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Comment_User_Id", "User", "Id")
                .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
