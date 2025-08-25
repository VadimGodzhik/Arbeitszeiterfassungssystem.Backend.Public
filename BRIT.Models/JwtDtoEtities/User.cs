using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Models.JwtDtoEtities
{
    public class User
    {
        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;


        public byte[] KennwortHash { get; set; }
        public byte[] KennwortSalt { get; set; }
    }
}
