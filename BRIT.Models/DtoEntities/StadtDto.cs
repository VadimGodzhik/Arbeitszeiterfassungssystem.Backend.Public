using BRIT.Models.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.DtoEntities
{
    public class StadtDto : BaseDtoEntities
    {
        public string Ort { get; set; } = string.Empty;

        public string Postleitzahl { get; set; } = string.Empty;
    }
}
