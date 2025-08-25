using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIT.Dal.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class StadtOnDeleteClientSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadt_HausanschriftId",
                schema: "dbo",
                table: "Hausanschriften");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadt_HausanschriftId",
                schema: "dbo",
                table: "Hausanschriften",
                column: "StadtId",
                principalSchema: "dbo",
                principalTable: "Städte",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadt_HausanschriftId",
                schema: "dbo",
                table: "Hausanschriften");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadt_HausanschriftId",
                schema: "dbo",
                table: "Hausanschriften",
                column: "StadtId",
                principalSchema: "dbo",
                principalTable: "Städte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
