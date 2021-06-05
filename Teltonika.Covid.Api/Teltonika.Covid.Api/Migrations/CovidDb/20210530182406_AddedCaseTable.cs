using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teltonika.Covid.Api.Migrations.CovidDb
{
    public partial class AddedCaseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeBracket",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeBracket", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    object_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    AgeBracketId = table.Column<int>(type: "int", nullable: true),
                    MunicipalityId = table.Column<int>(type: "int", nullable: true),
                    confirmation_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    case_code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Y = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    X = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.object_id);
                    table.ForeignKey(
                        name: "FK_Cases_AgeBracket_AgeBracketId",
                        column: x => x.AgeBracketId,
                        principalTable: "AgeBracket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_AgeBracketId",
                table: "Cases",
                column: "AgeBracketId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_GenderId",
                table: "Cases",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_MunicipalityId",
                table: "Cases",
                column: "MunicipalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "AgeBracket");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Municipality");
        }
    }
}
