using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mona.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportLines",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 4096, nullable: false),
                    Location = table.Column<string>(maxLength: 1024, nullable: false),
                    Responsible = table.Column<string>(maxLength: 1024, nullable: false),
                    TargetDate = table.Column<DateTimeOffset>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportLines_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportLines_ReportId",
                table: "ReportLines",
                column: "ReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportLines");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
