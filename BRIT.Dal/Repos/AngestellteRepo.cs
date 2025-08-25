using System.Collections.Generic;
using System.Data;
using System.Linq;
using BRIT.Dal.EfStructures;
using BRIT.Models.Entities;
using BRIT.Dal.Repos.Base;
using BRIT.Dal.Repos.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BRIT.Models;
using BRIT.Models.ViewModels;

namespace BRIT.Dal.Repos
{
    public class AngestellteRepo : BaseRepo<Angestellte>, IAngestellteRepo
    {
        public AngestellteRepo(ApplicationDbContext context) : base(context) { }
        internal AngestellteRepo(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public override IEnumerable<Angestellte> GetAll()
            => Table1.OrderBy(e => e.Nachname).OrderBy(e => e.Nachname);

        public override IEnumerable<Angestellte> GetAllIgnoreQueryFilters()
            => Table1.OrderBy(e => e.Nachname).OrderBy(e => e.Nachname).IgnoreQueryFilters();

        /*
        public IEnumerable<Angestellte> GetAllBy(int arbeitsortId)
        {
            List<Arbeitsort> arbeitsorts = new List<Arbeitsort>();
            List<AngestellteArbeitsortViewModel> angestellteArbeitsors = new List<AngestellteArbeitsortViewModel>();


            return Table.Include(e => e.Arbeitsorts).Where(a => a.Arbeitsorts.)
            //return Table.Where(x => x.MakeId == makeId).Include(c => c.MakeNavigation).OrderBy(c => c.PetName);
        }
        */


        /* Ein Beispiel, wie man eine gespeicherte Procedure implementieren kann
        public string GetPetName(int id)
        {
            var parameterId = new SqlParameter
            {
                ParameterName = "@carId",
                SqlDbType = SqlDbType.Int,
                Value = id,
            };

            var parameterName = new SqlParameter
            {
                ParameterName = "@petName",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Direction = ParameterDirection.Output
            };

            _ = Context.Database
                .ExecuteSqlRaw("EXEC [dbo].[GetPetName] @carId, @petName OUTPUT", parameterId, parameterName);
            return (string)parameterName.Value;
        }
        */
        //public virtual T? Find(int? id) => Table.Find(id);
        public override Angestellte? Find(int? id)
            => (Angestellte?) Table1.IgnoreQueryFilters().Where(a => a.Id == id);
        

    }
}
