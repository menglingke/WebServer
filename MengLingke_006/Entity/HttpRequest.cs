using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengLingke_006.Entity
{
    class HttpRequest
    {
        public HttpRequest(string requestText) 
        {
            string[] lines = requestText.Replace("\r\n","\r").Split('\r');
            string[] requestLines = lines[0].Split(' ');
            if (requestLines.Length>=2) 
            {
                HttpMethod = requestLines[0];
                Url = requestLines[1];
                HttpVersion = requestLines[2];
            }
            
        }

        public object HttpMethod { get; set; }
        public string Url { get;set; }
        public string HttpVersion { get; set; }
        public Dictionary<string, string> HeaderDictionary { get; set; }
        public Dictionary<string, string> BodyDictionary { get; set; }
    }
}
