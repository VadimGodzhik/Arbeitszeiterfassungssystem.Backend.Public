using BRIT.Models.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.DtoEntities.DtoWhithId
{
    public class StadtDtoWithId : BaseDtoEntitiesWithId
    {
        public string Ort { get; set; } = string.Empty;
        public string Postleitzahl { get; set; } = string.Empty;
    }
}
