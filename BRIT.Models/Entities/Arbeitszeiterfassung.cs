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
    [Table("Arbeitszeiterfassungen", Schema = "dbo")]
    public partial class Arbeitszeiterfassung : BaseEntities
    {
        [Required]
        public DateTime DatumUrzeit {  get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; } = string.Empty;

        public int AngestellteId { get; set; }

        [ForeignKey(nameof(AngestellteId))]
        [InverseProperty(nameof(Angestellte.Arbeitszeiterfassungs))]
        public Angestellte? AngestellteNavigation { get; set; }

        [InverseProperty(nameof(Fundort.ArbeitszeiterfassungNavigation))]
        public Fundort FundortNavigation { get; set; }
    }
}
