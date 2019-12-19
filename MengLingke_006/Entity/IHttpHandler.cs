using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengLingke_006.Entity
{
    interface IHttpHandler
    {
        void ProcessRequest(HttpContext context);
    }
}
