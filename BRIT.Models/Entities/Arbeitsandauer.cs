using BRIT.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BRIT.Models.Entities
{
    public partial class Arbeitsandauer : BaseEntities
    {
        [Required]
        public DateTime Datum { get; set; }

        [Required, StringLength(50)]
        public string Arbeitszeit { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Pausen { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Überstunden { get; set; } = string.Empty;

        [Required]
        public int AngestellteId { get; set; }

        [ForeignKey(nameof(AngestellteId))]
        [InverseProperty(nameof(Angestellte.Arbeitsandauers))]
        public Angestellte? AngestellteNavigation { get; set; }

    }
}
