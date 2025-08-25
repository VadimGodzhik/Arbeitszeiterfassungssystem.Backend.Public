using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIT.Services.ApiWrapper
{
    public class ApiServiceSettings
    {
        public ApiServiceSettings() { }

        public string Uri { get; set; }
        public string AngestellteBaseUri { get; set; }
        public string ArbeitsandauerBaseUri { get; set; }
        public string ArbeitsortBaseUri { get; set; }
        public string ArbeitszeiterfassungBaseUri { get; set; }
        public string FundortBaseUri { get; set; }
        public string KennwortBaseUri { get; set; }
        public string RolleBaseUri { get; set; }
        public string HausanschriftBaseUri { get; set; }
        public string StadtBaseUri { get; set; }
    }
}
