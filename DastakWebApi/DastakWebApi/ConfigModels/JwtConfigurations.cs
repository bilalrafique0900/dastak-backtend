using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DastakWebApi.ConfigModels
{
    public class JwtConfigurations
    {
        
        public static string Section = "JwtConfigurations";
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpirationTimeInMinutes { get; set; }
    }
}
