using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class SeedingData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Users SET City = 'Velenje', Country = 'Slovenia', Created = '2022-03-05 00:00:01', DOB = '1998-08-03 00:00:00', Description = 'Zdravo!', Gender = 'Male', Interests = 'Female', LastActive = '0001-01-01 00:00:01'  WHERE Id = 3; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
