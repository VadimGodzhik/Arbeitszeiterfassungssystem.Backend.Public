using BRIT.Models.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.DtoEntities
{
    public class ArbeitszeiterfassungDto : BaseDtoEntities
    {
        public DateTime DatumUrzeit { get; set; }

        public string Status { get; set; } = string.Empty;

        public int AngestellteId { get; set; }
    }
}
