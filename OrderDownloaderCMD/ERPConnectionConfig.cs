using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace OrderDownloaderCMD
{
    internal class ERPConnectionConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string UserPassword { get; set; }
        public string Operator { get; set; }
        public string OperatorPassword { get; set; }
    }
}