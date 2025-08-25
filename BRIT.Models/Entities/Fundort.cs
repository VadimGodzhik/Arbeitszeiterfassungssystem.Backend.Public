using BRIT.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BRIT.Models.Entities
{
    [Table("Fundorte")]
    [Index(nameof(ArbeitszeiterfassungId), Name = "IX_Fundorts_ArbeitszeiterfassungId", IsUnique = true)]
    public partial class Fundort : BaseEntities
    {
        [Required, StringLength(50)]
        public string Ort { get; set; }

        [Required, StringLength(20)]
        public string Postleitzahl { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string Straße { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Hausnummer { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Status { get; set; } = string.Empty;

        public int ArbeitszeiterfassungId { get; set; }

        [ForeignKey(nameof(ArbeitszeiterfassungId))]
        [InverseProperty(nameof(Arbeitszeiterfassung.FundortNavigation))]
        public Arbeitszeiterfassung ArbeitszeiterfassungNavigation { get; set; }
    }
}
