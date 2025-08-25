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
    [Table("Arbeitsorte", Schema = "dbo")]
    public partial class Arbeitsort : BaseEntities
    {
        [Required, StringLength(50)]
        public string Ort { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Postleitzahl { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string Straße { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Hausnummer { get; set; } = string.Empty;


        [JsonIgnore]
        [InverseProperty(nameof(Angestellte.Arbeitsorts))]
        public List<Angestellte> Angestelltes { get; set; } = new List<Angestellte>();

    }
}
