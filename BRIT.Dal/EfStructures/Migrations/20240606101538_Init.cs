using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIT.Dal.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Angestelltes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IstAngestellt = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angestelltes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arbeitsorte",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ort = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postleitzahl = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Straße = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Hausnummer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arbeitsorte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rollen",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rollen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Städte",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ort = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postleitzeit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Städte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arbeitsandauers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arbeitszeit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pausen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Überstunden = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AngestellteId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arbeitsandauers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Angestellte_ArbeitsandauerId",
                        column: x => x.AngestellteId,
                        principalTable: "Angestelltes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Arbeitszeiterfassungen",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumUrzeit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AngestellteId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arbeitszeiterfassungen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Angestellte_ArbeitszeiterfassungId",
                        column: x => x.AngestellteId,
                        principalTable: "Angestelltes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kennwörter",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zeichenfolge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AngestellteId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kennwörter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kennwörter_Angestelltes_AngestellteId",
                        column: x => x.AngestellteId,
                        principalTable: "Angestelltes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AngestellteArbeitsorte",
                columns: table => new
                {
                    AngestellteId = table.Column<int>(type: "int", nullable: false),
                    ArbeitsortId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AngestellteArbeitsorte", x => new { x.AngestellteId, x.ArbeitsortId });
                    table.ForeignKey(
                        name: "FK_AngestellteArbeitsorte_Angestellte_AngestellteId",
                        column: x => x.AngestellteId,
                        principalTable: "Angestelltes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AngestellteArbeitsorte_Arbeitsorte_ArbeitsortId",
                        column: x => x.ArbeitsortId,
                        principalSchema: "dbo",
                        principalTable: "Arbeitsorte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AngestellteRollen",
                columns: table => new
                {
                    AngestellteId = table.Column<int>(type: "int", nullable: false),
                    RolleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AngestellteRollen", x => new { x.AngestellteId, x.RolleId });
                    table.ForeignKey(
                        name: "FK_AngestellteRollen_Angestelltes_AngestellteId",
                        column: x => x.AngestellteId,
                        principalTable: "Angestelltes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AngestellteRollen_Rolles_RolleId",
                        column: x => x.RolleId,
                        principalSchema: "dbo",
                        principalTable: "Rollen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hausanschriften",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Straße = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Hausnummer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StadtId = table.Column<int>(type: "int", nullable: false),
                    AngestellteId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hausanschriften", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hausanschriften_Angestelltes_AngestellteId",
                        column: x => x.AngestellteId,
                        principalTable: "Angestelltes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stadt_HausanschriftId",
                        column: x => x.StadtId,
                        principalSchema: "dbo",
                        principalTable: "Städte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fundorte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ort = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postleitzahl = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Straße = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Hausnummer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArbeitszeiterfassungId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fundorte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fundorte_Arbeitszeiterfassungen_ArbeitszeiterfassungId",
                        column: x => x.ArbeitszeiterfassungId,
                        principalSchema: "dbo",
                        principalTable: "Arbeitszeiterfassungen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AngestellteArbeitsorte_ArbeitsortId",
                table: "AngestellteArbeitsorte",
                column: "ArbeitsortId");

            migrationBuilder.CreateIndex(
                name: "IX_AngestellteRollen_RolleId",
                table: "AngestellteRollen",
                column: "RolleId");

            migrationBuilder.CreateIndex(
                name: "IX_Arbeitsandauers_AngestellteId",
                table: "Arbeitsandauers",
                column: "AngestellteId");

            migrationBuilder.CreateIndex(
                name: "IX_Arbeitszeiterfassungen_AngestellteId",
                schema: "dbo",
                table: "Arbeitszeiterfassungen",
                column: "AngestellteId");

            migrationBuilder.CreateIndex(
                name: "IX_Fundort_ArbeitszeiterfassungId",
                table: "Fundorte",
                column: "ArbeitszeiterfassungId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fundorts_ArbeitszeiterfassungId",
                table: "Fundorte",
                column: "ArbeitszeiterfassungId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hausanschrift_AngestellteId",
                schema: "dbo",
                table: "Hausanschriften",
                column: "AngestellteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hausanschriften_StadtId",
                schema: "dbo",
                table: "Hausanschriften",
                column: "StadtId");

            migrationBuilder.CreateIndex(
                name: "IX_Hausanschrifts_AngestellteId",
                schema: "dbo",
                table: "Hausanschriften",
                column: "AngestellteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kennwort_AngestellteId",
                schema: "dbo",
                table: "Kennwörter",
                column: "AngestellteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kennworts_AngestellteId",
                schema: "dbo",
                table: "Kennwörter",
                column: "AngestellteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AngestellteArbeitsorte");

            migrationBuilder.DropTable(
                name: "AngestellteRollen");

            migrationBuilder.DropTable(
                name: "Arbeitsandauers");

            migrationBuilder.DropTable(
                name: "Fundorte");

            migrationBuilder.DropTable(
                name: "Hausanschriften",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Kennwörter",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Arbeitsorte",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Rollen",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Arbeitszeiterfassungen",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Städte",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Angestelltes");
        }
    }
}
