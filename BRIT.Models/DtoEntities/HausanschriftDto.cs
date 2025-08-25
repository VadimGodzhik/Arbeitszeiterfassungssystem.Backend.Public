using BRIT.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRIT.Models.DtoEntities.Base;

namespace BRIT.Models.DtoEntities
{
    public class HausanschriftDto : BaseDtoEntities
    {
        public string Straße { get; set; } = string.Empty;

        public string Hausnummer { get; set; } = string.Empty;

        public int StadtId { get; set; }

        public int AngestellteId { get; set; }
    }
}
