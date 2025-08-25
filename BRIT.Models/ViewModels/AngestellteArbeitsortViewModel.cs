using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.ViewModels
{
        [Keyless]
        public class AngestellteArbeitsortViewModel
        {
            public int? AngestellteId { get; set; }
            public bool? IstAngestellt { get; set; }
            public string? Vorname { get; set; }
            public string? Nachname { get; set; }
            
        
            public int? ArbeitsortId { get; set; }
            public string? Ort { get; set; }
            public string? Postleitzahl { get; set; }
            public string? Straße { get; set; }

        }
}
