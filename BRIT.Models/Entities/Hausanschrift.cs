using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BRIT.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BRIT.Models.Entities
{
    [Table("Hausanschriften", Schema = "dbo")]
    [Index(nameof(AngestellteId), Name = "IX_Hausanschrifts_AngestellteId", IsUnique = true)]

    public partial class Hausanschrift : BaseEntities
    {
        [Required, StringLength(200)]
        public string Straße { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Hausnummer { get; set; } = string.Empty;


        // Many to One: Rücknavigation mit Foreignkey
        public int StadtId { get; set; }

        [ForeignKey(nameof(StadtId))]
        [InverseProperty(nameof(Stadt.Hausanschrifts))]
        public Stadt StadtNavigation { get; set; }


        // One to One: Rücknavigation mit Foregnkey
        public int AngestellteId { get; set; }

        [ForeignKey(nameof(AngestellteId))]
        [InverseProperty(nameof(Angestellte.HausanschriftNavigation))]
        public Angestellte AngestellteNavigation { get; set; }
    }
}
