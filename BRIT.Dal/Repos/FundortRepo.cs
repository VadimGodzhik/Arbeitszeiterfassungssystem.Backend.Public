using BRIT.Dal.EfStructures;
using BRIT.Dal.Repos.Base;
using BRIT.Dal.Repos.Interfaces;
using BRIT.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Dal.Repos
{
    public class FundortRepo : BaseRepo<Fundort>, IFundortRepo
    {
        public FundortRepo(ApplicationDbContext context) : base(context) { }
        internal FundortRepo(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
