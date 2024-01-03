using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class HeaderServer
    {
        public HeaderServer(string status, string contentType) 
        {
            Status = status;
            ContentType = contentType;
        }
        public string Status { get; set; }
        public string ContentType { get; set; }
        public string GetText()
        {
            return Status + Environment.NewLine + 
                   ContentType + Environment.NewLine;
        }
    }
}
