using BRIT.Dal.EfStructures;
using BRIT.Dal.Repos.Base;
using BRIT.Dal.Repos.Interfaces;
using BRIT.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BRIT.Dal.Exceptions;

namespace BRIT.Dal.Repos
{
    public class StadtRepo : BaseRepo<Stadt>, IStadtRepo
    {
        public StadtRepo(ApplicationDbContext context) : base(context) { }
        internal StadtRepo(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }


}
