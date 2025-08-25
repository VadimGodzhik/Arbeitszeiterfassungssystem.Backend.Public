using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIT.Dal.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class SQL_EmptyMigrationsMethoden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            MigrationHelpers.CreateAngestellteArbeitsandauerView(migrationBuilder);
            MigrationHelpers.CreateAngestellteArbeitszeiterfassungenView(migrationBuilder);
            MigrationHelpers.CreateAngestellteArbeitsorteView(migrationBuilder);
            MigrationHelpers.CreateAngestellteRollenView(migrationBuilder);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            MigrationHelpers.DropAngestellteArbeitsandauerView(migrationBuilder);
            MigrationHelpers.DropAngestellteArbeitszeiterfassungenView(migrationBuilder);
            MigrationHelpers.DropAngestellteArbeitsorteView(migrationBuilder);
            MigrationHelpers.DropAngestellteRollenView(migrationBuilder);
        }
    }
}
