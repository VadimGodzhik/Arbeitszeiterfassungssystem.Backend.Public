using BRIT.Dal.EfStructures;
using BRIT.Dal.Repos.Base;
using BRIT.Dal.Repos.Interfaces;
using BRIT.Models.Entities;
using BRIT.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Dal.Repos
{
    public class ArbeitsandauerRepo : BaseRepo<Arbeitsandauer>, IArbeitsandauerRepo
    {
        public ArbeitsandauerRepo(ApplicationDbContext context) : base(context) { }
        internal ArbeitsandauerRepo(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public IQueryable<AngestellteArbeitsandauerViewModel> GetArbeitsandauerViewModels()
        {
            return Context.AngestellteArbeitsandauerViewModels!.AsQueryable();
        }


        

    }
}
