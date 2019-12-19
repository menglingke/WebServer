using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MengLingke_006.Entity
{
    class HttpApplication : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request.Url)) 
            {
                return;
            }
            string bastPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = Path.Combine(bastPath+"\\MyWebSite",context.Request.Url.TrimStart('/'));
            string fileExtension = Path.GetExtension(context.Request.Url);
            if (fileExtension.Equals(".aspx")||fileExtension.Equals(".ashx")) 
            {
                string className = Path.GetFileNameWithoutExtension(context.Request.Url);
                IHttpHandler handler = Assembly.Load("MengLingke_006").CreateInstance("MengLingke_006.Page." + className) as IHttpHandler;
                handler.ProcessRequest(context);
                return;
            }
            if (!File.Exists(fileName))
            {
                context.Response.StateCode = "404";
                context.Response.StateDescription = "Not Found";
                context.Response.ContentType = "text/html";
                string notExistHtml = Path.Combine(bastPath, @"MyWebSite\notfound.html");
                context.Response.Body = File.ReadAllBytes(notExistHtml);
            }
            else 
            {
                context.Response.StateCode = "200";
                context.Response.StateDescription = "OK";
                context.Response.ContentType = GetContextTYpe(Path.GetExtension(context.Request.Url));
            }
        }

        private string GetContextTYpe(string fileExtention)
        {
            string type = "text/html;charset=UTF-8";
            switch (fileExtention) 
            {
                case ".aspx":
                case ".html":
                case ".htm":
                    type = "text/html;charset=UTF-8";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                case ".gif":
                    type = "image/gif";
                    break;
                case ".jpg":
                case ".jepg":
                    type = "image/jepg";
                    break;
                case ".css":
                    type = "text/css";
                    break;
                case ".js":
                    type = "application/x-javascript";
                    break;
                default:
                    type = "text/plain;charset=gbk";
                    break;
            }
            return type;
        }
    }
}
