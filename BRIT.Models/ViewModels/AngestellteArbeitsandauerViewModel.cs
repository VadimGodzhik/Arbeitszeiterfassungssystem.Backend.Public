using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BRIT.Models.ViewModels
{
    [Keyless]
    public class AngestellteArbeitsandauerViewModel
    {
        public DateTime? Datum { get; set; }
        public int? AngestellteId { get; set; }
        public bool? IstAngestellt { get; set; }
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? Arbeitszeit { get; set; }
        public string? Pausen { get; set; }
        public string? Überstunden { get; set; }
        public string? Gesamtandauer { get; set; }

        //public string FullDetail => $"{Vorname} {Nachname} ordered a {Datum}";
    }
}
