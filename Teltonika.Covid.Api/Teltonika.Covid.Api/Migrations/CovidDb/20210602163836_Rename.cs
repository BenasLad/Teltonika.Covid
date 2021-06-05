using Microsoft.EntityFrameworkCore.Migrations;

namespace Teltonika.Covid.Api.Migrations.CovidDb
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_AgeBracket_AgeBracketId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Gender_GenderId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Municipality_MunicipalityId",
                table: "Cases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Municipality",
                table: "Municipality");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gender",
                table: "Gender");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgeBracket",
                table: "AgeBracket");

            migrationBuilder.RenameTable(
                name: "Municipality",
                newName: "Municipalities");

            migrationBuilder.RenameTable(
                name: "Gender",
                newName: "Genders");

            migrationBuilder.RenameTable(
                name: "AgeBracket",
                newName: "AgeBrackets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Municipalities",
                table: "Municipalities",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genders",
                table: "Genders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgeBrackets",
                table: "AgeBrackets",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_AgeBrackets_AgeBracketId",
                table: "Cases",
                column: "AgeBracketId",
                principalTable: "AgeBrackets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Genders_GenderId",
                table: "Cases",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Municipalities_MunicipalityId",
                table: "Cases",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_AgeBrackets_AgeBracketId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Genders_GenderId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Municipalities_MunicipalityId",
                table: "Cases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Municipalities",
                table: "Municipalities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genders",
                table: "Genders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgeBrackets",
                table: "AgeBrackets");

            migrationBuilder.RenameTable(
                name: "Municipalities",
                newName: "Municipality");

            migrationBuilder.RenameTable(
                name: "Genders",
                newName: "Gender");

            migrationBuilder.RenameTable(
                name: "AgeBrackets",
                newName: "AgeBracket");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Municipality",
                table: "Municipality",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gender",
                table: "Gender",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgeBracket",
                table: "AgeBracket",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_AgeBracket_AgeBracketId",
                table: "Cases",
                column: "AgeBracketId",
                principalTable: "AgeBracket",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Gender_GenderId",
                table: "Cases",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Municipality_MunicipalityId",
                table: "Cases",
                column: "MunicipalityId",
                principalTable: "Municipality",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
