using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class SeedingData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Users SET City = 'Cacak', Country = 'Serbia', Created = '2022-03-05 00:00:00', DOB = '1999-03-05 00:00:00', Description = 'Cao!', Gender = 'Male', Interests = 'Female', LastActive = '0001-01-01 00:00:00'  WHERE Id = 1; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
