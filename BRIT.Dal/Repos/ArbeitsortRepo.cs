using BRIT.Dal.EfStructures;
using BRIT.Dal.Repos.Base;
using BRIT.Dal.Repos.Interfaces;
using BRIT.Models.Entities;
using BRIT.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Dal.Repos
{
    public class ArbeitsortRepo : BaseRepo<Arbeitsort>, IArbeitsortRepo
    {
        public ArbeitsortRepo(ApplicationDbContext context) : base(context) { }
        internal ArbeitsortRepo(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
