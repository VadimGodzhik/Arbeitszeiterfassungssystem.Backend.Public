using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BRIT.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;


namespace BRIT.Models.Entities
{
    [Table("Rollen", Schema = "dbo")]
    public partial class Rolle : BaseEntities
    {
        [Required, StringLength(50)]
        public string Bezeichnung { get; set; } = string.Empty;



        [JsonIgnore]
        [InverseProperty(nameof(Angestellte.Rolles))]
        public IEnumerable<Angestellte> Angestelltes { get; set; } = new List<Angestellte>();
    }
}
