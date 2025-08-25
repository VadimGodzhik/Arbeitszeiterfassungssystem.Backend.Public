using BRIT.Models.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.DtoEntities
{
    public class KennwortDto : BaseDtoEntities
    {
        public string Zeichenfolge { get; set; } = string.Empty;
        public int AngestellteId { get; set; }
    }
}
