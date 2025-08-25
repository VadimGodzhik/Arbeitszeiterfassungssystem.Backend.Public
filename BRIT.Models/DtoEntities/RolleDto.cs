using BRIT.Models.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.DtoEntities
{
    public class RolleDto : BaseDtoEntities
    {
        public string Bezeichnung { get; set; } = string.Empty;
    }
}
