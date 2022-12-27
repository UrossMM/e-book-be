using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace diplomski.Migrations
{
    public partial class admindefaults_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FatteningText",
                table: "AdminDefaults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeepingFitText",
                table: "AdminDefaults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeightLossText",
                table: "AdminDefaults",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatteningText",
                table: "AdminDefaults");

            migrationBuilder.DropColumn(
                name: "KeepingFitText",
                table: "AdminDefaults");

            migrationBuilder.DropColumn(
                name: "WeightLossText",
                table: "AdminDefaults");
        }
    }
}
