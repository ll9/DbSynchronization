using Microsoft.EntityFrameworkCore.Migrations;

namespace DbSyncAlgorithm.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Person Values(NEWID(), 'Hans', 33, GETDATE ())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete FROM Person WHERE Name = 'Hans' AND Age = 33");
        }
    }
}
