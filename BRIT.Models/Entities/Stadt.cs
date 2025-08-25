using BRIT.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.Entities
{
    [Table("Städte", Schema = "dbo")]
    public partial class Stadt : BaseEntities
    {
        [Required, StringLength(50)]
        public string Ort { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Postleitzahl { get; set; } = string.Empty;


        // One to Many: Hauptnavigation
        [InverseProperty(nameof(Hausanschrift.StadtNavigation))]
        public List<Hausanschrift> Hausanschrifts { get; set; } = new List<Hausanschrift>();
    }
}
