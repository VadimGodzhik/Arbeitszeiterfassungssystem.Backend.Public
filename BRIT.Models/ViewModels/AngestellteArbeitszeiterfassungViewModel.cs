using BRIT.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.ViewModels
{
    [Keyless]
    public class AngestellteArbeitszeiterfassungViewModel
    {
   
        public DateTime? DatumUrzeit { get; set; }
        public int? AngestellteId { get; set; }
        public bool? IstAngestellt { get; set; }
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? ArbeitszeiterfassungStatus { get; set; }
        public string? Ort { get; set; }
        public string? Postleitzahl { get; set; }
        public string? Straße { get; set; }
        public string? FundortStatus { get; set; }


    }
}
