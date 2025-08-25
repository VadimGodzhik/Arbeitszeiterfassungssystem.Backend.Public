using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BRIT.Dal.EfStructures
{
    public static class MigrationHelpers
    {
        //
        public static void CreateAngestellteArbeitsandauerView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                exec (N' 
                CREATE VIEW [dbo].[AngestellteArbeitsandauerView]
                AS
                    SELECT dbo.Arbeitsandauers.Datum, dbo.Arbeitsandauers.Id as ArbeitsandauerId, dbo.Arbeitsandauers.AngestellteId, dbo.Angestelltes.IstAngestellt, dbo.Angestelltes.Vorname, dbo.Angestelltes.Nachname, dbo.Arbeitsandauers.Arbeitszeit, dbo.Arbeitsandauers.Pausen, dbo.Arbeitsandauers.Überstunden
                    FROM   dbo.Arbeitsandauers
                    INNER JOIN dbo.Angestelltes ON dbo.Arbeitsandauers.AngestellteId = dbo.Angestelltes.Id
                ')");
        }

        public static void DropAngestellteArbeitsandauerView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("EXEC (N' DROP VIEW [dbo].[AngestellteArbeitsandauerView] ')");
        }


        //
        public static void CreateAngestellteArbeitszeiterfassungenView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                exec (N' 
                CREATE VIEW [dbo].[AngestellteArbeitszeiterfassungenView]
                AS
                    SELECT dbo.Arbeitszeiterfassungen.DatumUrzeit, dbo.Arbeitszeiterfassungen.AngestellteId, dbo.Angestelltes.IstAngestellt, dbo.Angestelltes.Vorname, dbo.Angestelltes.Nachname, dbo.Arbeitszeiterfassungen.Status As ArbeitszeiterfassungStatus, dbo.Fundorte.Ort, dbo.Fundorte.Postleitzahl, dbo.Fundorte.Straße, dbo.Fundorte.Hausnummer, dbo.Fundorte.Status As FundortStatus
                    FROM   dbo.Arbeitszeiterfassungen
                    INNER JOIN dbo.Angestelltes ON dbo.Arbeitszeiterfassungen.AngestellteId = dbo.Angestelltes.Id
                    INNER JOIN dbo.Fundorte ON dbo.Arbeitszeiterfassungen.Id = dbo.Fundorte.ArbeitszeiterfassungId
                ')");
        }

        public static void DropAngestellteArbeitszeiterfassungenView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("EXEC (N' DROP VIEW [dbo].[AngestellteArbeitszeiterfassungenView] ')");
        }


        //
        public static void CreateAngestellteArbeitsorteView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                exec (N' 
                CREATE VIEW [dbo].[AngestellteArbeitsorteView]
                AS
                    SELECT dbo.AngestellteArbeitsorte.AngestellteId, dbo.Angestelltes.IstAngestellt, dbo.Angestelltes.Vorname, dbo.Angestelltes.Nachname, dbo.AngestellteArbeitsorte.ArbeitsortId, dbo.Arbeitsorte.Ort, dbo.Arbeitsorte.Postleitzahl, dbo.Arbeitsorte.Straße, dbo.Arbeitsorte.Hausnummer
                    FROM   dbo.AngestellteArbeitsorte
                    INNER JOIN dbo.Angestelltes ON dbo.AngestellteArbeitsorte.AngestellteId = dbo.Angestelltes.Id
                    INNER JOIN dbo.Arbeitsorte ON dbo.AngestellteArbeitsorte.ArbeitsortId = dbo.Arbeitsorte.Id
                ')");
        }

        public static void DropAngestellteArbeitsorteView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("EXEC (N' DROP VIEW [dbo].[AngestellteArbeitsorteView] ')");
        }


        //
        public static void CreateAngestellteRollenView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                exec (N' 
                CREATE VIEW [dbo].[AngestellteRollenView]
                AS
                    SELECT dbo.AngestellteRollen.AngestellteId, dbo.Angestelltes.IstAngestellt, dbo.Angestelltes.Vorname, dbo.Angestelltes.Nachname, AngestellteRollen.RolleId, dbo.Rollen.Bezeichnung
                    FROM   dbo.AngestellteRollen
                    INNER JOIN dbo.Angestelltes ON dbo.AngestellteRollen.AngestellteId = dbo.Angestelltes.Id
                    INNER JOIN dbo.Rollen ON dbo.AngestellteRollen.RolleId = dbo.Rollen.Id
                ')");
        }

        public static void DropAngestellteRollenView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("EXEC (N' DROP VIEW [dbo].[AngestellteRollenView] ')");
        }
    }
}
