using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace NachtWal
{
    /// <summary>
    /// Anti-XSS Attack
    /// </summary>
    partial class XSSAuditor
    {
        private HttpApplication App;
        private string HttpResponseBody = String.Empty;

        public void CheckXSS(HttpApplication Application, string ResponseBody)
        {
            App = Application;
            HttpResponseBody = ResponseBody;
            CheckXSSKeyValue(this.App.Request.QueryString); // QueryString
            CheckXSSKeyValue(this.App.Request.Form); // Body
        }

        private void CheckXSSKeyValue(NameValueCollection Param)
        {
            foreach (string key in Param.Keys)
            {
                XSSReflected(key); // Key
                XSSReflected(Param[key]); // Value
            }
        }

        private void ResponseRewrite(string Title, string Head, string Message)
        {
            StringBuilder Body = new StringBuilder();
            Body.Append("<!DOCTYPE html>\n");
            Body.Append("<html>\n");
            Body.Append("\t<head>\n");
            Body.Append("\t\t<title>" + Title + "</title>\n");
            Body.Append("\t</head>\n");
            Body.Append("\t<body>\n");
            Body.Append("\t\t<h1>" + Head + "</h1>\n");
            Body.Append("\t\t<p>" + Message + "</p>\n");
            Body.Append("\t\t<hr>\n\t\t<address>NachtWal/" + AssemblyInformation.Version + " (" + AssemblyInformation.Release + ") Server at " + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + " Port " + HttpContext.Current.Request.ServerVariables["SERVER_PORT"] + "</address>\n");
            Body.Append("\t</body>\n");
            Body.Append("</html>");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(Body);
        }

        public void Dispose()
        {
            HttpResponseBody = String.Empty;
        }
    }
}