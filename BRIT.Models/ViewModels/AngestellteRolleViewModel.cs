using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.ViewModels
{
    [Keyless]
    public class AngestellteRolleViewModel
    {
        public int? AngestellteId { get; set; }
        public bool? IstAngestellt { get; set; }
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }


        public int? RolleId { get; set; }
        public string? Bezeichnung { get; set; }

    }
}
