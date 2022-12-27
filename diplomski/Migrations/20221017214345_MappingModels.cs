using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace diplomski.Migrations
{
    public partial class MappingModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminDatas",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminDatas", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "AdminDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    UnpersonalizedText = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminDefaults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "UserDatas",
                columns: table => new
                {
                    Mail = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    ActivityLevel = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    DailyNumberOfMeals = table.Column<int>(type: "int", nullable: false),
                    Additions = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDatas", x => x.Mail);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Ingredient = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Recipe = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    Mass = table.Column<int>(type: "int", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Templates_TemplateName",
                        column: x => x.TemplateName,
                        principalTable: "Templates",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_TemplateName",
                table: "Meals",
                column: "TemplateName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminDatas");

            migrationBuilder.DropTable(
                name: "AdminDefaults");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "UserDatas");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
