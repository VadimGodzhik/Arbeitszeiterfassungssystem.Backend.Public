using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIT.Dal.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class StadtPostleitzahl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Postleitzeit",
                schema: "dbo",
                table: "Städte",
                newName: "Postleitzahl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Postleitzahl",
                schema: "dbo",
                table: "Städte",
                newName: "Postleitzeit");
        }
    }
}
