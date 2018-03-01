using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONG.Utilitarios
{
    /// <summary>
    /// Summary description for DataReport
    /// </summary>
    public class DataReport : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}