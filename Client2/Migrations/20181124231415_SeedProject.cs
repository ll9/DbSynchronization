using Microsoft.EntityFrameworkCore.Migrations;

namespace Client.Migrations
{
    public partial class SeedProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Projects(Name) VALUES ('TestProject')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Projects WHERE Name = 'TestProject'");
        }
    }
}
