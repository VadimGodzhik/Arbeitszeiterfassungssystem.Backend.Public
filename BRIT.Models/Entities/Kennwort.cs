using BRIT.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BRIT.Models.Entities
{
    [Table("Kennwörter", Schema = "dbo")]
    [Index(nameof(AngestellteId), Name = "IX_Kennworts_AngestellteId", IsUnique = true)]
    public partial class Kennwort : BaseEntities
    {
        [Required, StringLength(50)]
        public string Zeichenfolge {get; set;} = string.Empty;

        public int AngestellteId { get; set;}

        [ForeignKey(nameof(AngestellteId))]
        [InverseProperty(nameof(Angestellte.KennwortNavigation))]
        public Angestellte AngestellteNavigation { get; set; }
    }
}
