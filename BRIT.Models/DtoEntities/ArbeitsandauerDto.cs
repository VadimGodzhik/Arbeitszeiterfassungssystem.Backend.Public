using BRIT.Models.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.DtoEntities
{
    public class ArbeitsandauerDto : BaseDtoEntities
    {
        public DateTime Datum { get; set; }

        public string Arbeitszeit { get; set; } = string.Empty;

        public string Pausen { get; set; } = string.Empty;

        public string Überstunden { get; set; } = string.Empty;

        public int AngestellteId { get; set; }
    }
}
