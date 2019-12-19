using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengLingke_006.Entity
{
    class HttpResponse
    {
        public string StateCode { get; set; }
        public string StateDescription { get; set; }
        public string ContentType { get; set; }
        public byte[] Body { get; set; }
        public byte[] GetResponseHeader() 
        {
            string strResquestHeader = string.Format(@"HTTP/1.1 {0} {1}
            Content-Type:{2}
            Accept-Ranges:bytes
            Sever:Microsoft-IIS/7.5
            X-Powered=BY:ASP.NET
            Date:{3}
            Content-Length:{4}
", StateCode, StateDescription, ContentType, string.Format("{0:R}", DateTime.Now), Body.Length);
            return Encoding.UTF8.GetBytes(strResquestHeader);
        }
    }
}
