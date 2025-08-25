using BRIT.Models.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.DtoEntities
{
    public class AngestellteDto : BaseDtoEntities
    {
        public string Vorname { get; set; } = string.Empty;

        public string Nachname { get; set; } = string.Empty;

        private bool? _istAngestellt;
    }
}
