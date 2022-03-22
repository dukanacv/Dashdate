using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class PhotoMigrationPlsWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Photos(Url, isMain, UserId) VALUES('https://i.ytimg.com/vi/i5k-ysXastA/hqdefault.jpg', 1, 3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
