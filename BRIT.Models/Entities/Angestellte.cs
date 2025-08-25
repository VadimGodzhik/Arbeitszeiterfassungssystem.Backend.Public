using BRIT.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BRIT.Models.Entities
{
    public class Angestellte : BaseEntities
    {
        [Required, StringLength(50)]
        public string Vorname { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Nachname { get; set; } = string.Empty;

        // Abfragefilter (queryfilter) ist zur Abbildung nur den zurzeit angestellten Mitarbeter verwendbar
        private bool? _istAngestellt;
        [DisplayName("Ist Angestellt")]
        public bool IstAngestellt
        {
            get => _istAngestellt ?? false;
            set => _istAngestellt = value;
        }

        /*
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? VollName { get; set; }
        */
        
        //One to Many
        [JsonIgnore]
        [InverseProperty(nameof(Arbeitszeiterfassung.AngestellteNavigation))]
        public IEnumerable<Arbeitszeiterfassung> Arbeitszeiterfassungs { get; set; } = new List<Arbeitszeiterfassung>();




        // Many to Many
        [JsonIgnore]
        [InverseProperty(nameof(Rolle.Angestelltes))]
        public IEnumerable<Rolle> Rolles { get; set; } = new List<Rolle>();


        [JsonIgnore]
        [InverseProperty(nameof(Arbeitsandauer.AngestellteNavigation))]
        public IEnumerable<Arbeitsandauer> Arbeitsandauers { get; set; } = new List<Arbeitsandauer>();

        [JsonIgnore]
        [InverseProperty(nameof(Arbeitsort.Angestelltes))]
        public List<Arbeitsort> Arbeitsorts { get; set; } = new List<Arbeitsort>();


        // One to One
        [InverseProperty(nameof(Kennwort.AngestellteNavigation))]
        public Kennwort KennwortNavigation { get; set; }

        [InverseProperty(nameof(Kennwort.AngestellteNavigation))]
        public Hausanschrift HausanschriftNavigation { get; set; }
    }
}
